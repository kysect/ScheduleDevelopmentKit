using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ItmoScheduleApiWrapper.Models;
using ItmoScheduleApiWrapper.Types;
using ScheduleAggregator.Ui.CustomElements;

namespace ScheduleAggregator.Ui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> GroupList = new ObservableCollection<string>();
        public ObservableCollection<int> UserIdList = new ObservableCollection<int>();

        public MainWindow()
        {
            InitializeComponent();

            GroupList.Add("M3101");
            GroupList.Add("M3107");
            InitLists();
            AddedGroupList.ItemsSource = GroupList;
            AddedUserList.ItemsSource = UserIdList;
        }

        public void InitLists()
        {
            var provider = new ScheduleItemProvider(GroupList, null);
            List<ScheduleItemModel> items = provider.GetItemsForGroup(UserIdList.ToList());

            InitList(OddItemList, DataWeekType.Odd, items);
            InitList(EvenItemList, DataWeekType.Even, items);
        }

        public void InitList(ListBox listBox, DataWeekType weekType, List<ScheduleItemModel> items)
        {
            List<DaySchedule> schedule = new List<DaySchedule>
            {
                new DaySchedule(items, weekType, DataDayType.Monday),
                new DaySchedule(items, weekType, DataDayType.Tuesday),
                new DaySchedule(items, weekType, DataDayType.Wednesday),
                new DaySchedule(items, weekType, DataDayType.Thursday),
                new DaySchedule(items, weekType, DataDayType.Friday),
                new DaySchedule(items, weekType, DataDayType.Saturday)
            };
            listBox.ItemsSource = schedule;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var newGroup = InputGroup.Text.ToUpper();
            if (!GroupList.Contains(newGroup))
            {
                GroupList.Add(newGroup);
                InitLists();
            }
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
            else
            {
                MessageBox.Show($"Error: {element.ToString()}");
            }
        }

        private void UserAddButton_OnClick(Object sender, RoutedEventArgs e)
        {
            Int32 id = int.Parse(InputUser.Text);
            if (!UserIdList.Contains(id))
            {
                UserIdList.Add(id);
                InitLists();
            }
        }
    }
}
