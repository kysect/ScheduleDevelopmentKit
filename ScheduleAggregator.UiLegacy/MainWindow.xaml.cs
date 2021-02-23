using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Kysect.ItmoScheduleSdk;
using Kysect.ItmoScheduleSdk.Models;
using Kysect.ItmoScheduleSdk.Types;
using ScheduleAggregator.Core.ScheduleItemProviders;
using ScheduleAggregator.Ui.CustomElements;


namespace ScheduleAggregator.Ui
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> GroupList = new ObservableCollection<string>();
        public ObservableCollection<int> UserIdList = new ObservableCollection<int>();

        private List<DayScheduleDescriptor> descriptors;
        private TableMasks Masks = new TableMasks();


        public MainWindow()
        {
            InitializeComponent();

            //GroupList.Add("M3401");
            //GroupList.Add("M3402");
            //GroupList.Add("M3403");
            //GroupList.Add("M3404");
            //GroupList.Add("M3405");
            //GroupList.Add("M3406");
            //GroupList.Add("M3407");
            //GroupList.Add("M3408");
            //GroupList.Add("M3409");
            //GroupList.Add("M3410");

            GroupList.Add("M3101");
            GroupList.Add("M3102");
            GroupList.Add("M3103");
            GroupList.Add("M3104");
            GroupList.Add("M3105");
            GroupList.Add("M3106");
            GroupList.Add("M3107");
            GroupList.Add("M3108");
            GroupList.Add("M3109");
            GroupList.Add("M3110");
            GroupList.Add("M3111");
            GroupList.Add("M3112");


            InitLists();

            AddedGroupList.ItemsSource = GroupList;
            AddedUserList.ItemsSource = UserIdList;
        }

        public void InitLists()
        {
            var provider = new ApiScheduleItemProvider(GroupList, UserIdList);
            List<ScheduleItemModel> items = provider.GetItems();
            descriptors = ScheduleApiExtensions.GroupElementsPerDay(items).ToList();
            PutMasks();
        }

        private void PutMasks()
        {
            var Shedule = new List<DayScheduleDescriptor>();

            foreach (var day in descriptors.Where(d => d.DataWeek == Masks.WeekType).ToList())
            {

                var MaskedScheduleItems = day.ScheduleItems;

                if (Masks.Room != default)
                    MaskedScheduleItems = MaskedScheduleItems.Where(l => l.Room == Masks.Room).ToList();
                if (Masks.StartTime != default)
                    MaskedScheduleItems = MaskedScheduleItems.Where(l => l.StartTime == Masks.StartTime).ToList();
                if (Masks.SubjectTitle != default)
                    MaskedScheduleItems = MaskedScheduleItems.Where(l => l.SubjectTime == Masks.SubjectTitle).ToList();
                if (Masks.Teacher != default)
                    MaskedScheduleItems = MaskedScheduleItems.Where(l => l.Teacher == Masks.Teacher).ToList();

                Shedule.Add(new DayScheduleDescriptor(day.DataDay, day.DataWeek, MaskedScheduleItems));

            }
            ItemList.ItemsSource = Shedule.Select(d => new DaySchedule(d)).ToList();
        }

        private void GroupAdd_Click(object sender, RoutedEventArgs e)
        {
            var newGroup = InputGroup.Text.ToUpper();
            if (!GroupList.Contains(newGroup))
            {
                GroupList.Add(newGroup);
                InitLists();
            }
        }


        private void OddWeek_Checked(object sender, RoutedEventArgs e)
        {
            Masks.WeekType = DataWeekType.Odd;
            PutMasks();
        }
        private void EvenWeek_Checked(object sender, RoutedEventArgs e)
        {
            Masks.WeekType = DataWeekType.Even;
            PutMasks();
        }

        private void AddedGroupList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count <= 0)
                return;

            var element = e.AddedItems[0];
            if (GroupList.Contains(element.ToString()))
            {
                GroupList.Remove(element.ToString());
                InitLists();
            }
        }

        private void UserAdd_OnClick(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(InputUser.Text);
            if (!UserIdList.Contains(id))
            {
                UserIdList.Add(id);
                InitLists();
            }
        }

        private void AddedUserList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count <= 0)
                return;

            int id = int.Parse(e.AddedItems[0].ToString());
            if (UserIdList.Contains(id))
            {
                UserIdList.Remove(id);
                InitLists();
            }
        }
    }
}
