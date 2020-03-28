﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ItmoScheduleApiWrapper.Models;
using ItmoScheduleApiWrapper.Types;
using ScheduleAggregator.Ui.CustomElements;

namespace ScheduleAggregator.Ui
{
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
            var provider = new ScheduleItemProvider(GroupList, UserIdList, null);
            List<ScheduleItemModel> items = provider.GetItemsForGroup();

            OddItemList.ItemsSource = new List<DaySchedule>
            {
                new DaySchedule(items, DataWeekType.Odd, DataDayType.Monday),
                new DaySchedule(items, DataWeekType.Odd, DataDayType.Tuesday),
                new DaySchedule(items, DataWeekType.Odd, DataDayType.Wednesday),
                new DaySchedule(items, DataWeekType.Odd, DataDayType.Thursday),
                new DaySchedule(items, DataWeekType.Odd, DataDayType.Friday),
                new DaySchedule(items, DataWeekType.Odd, DataDayType.Saturday)
            };

            EvenItemList.ItemsSource = new List<DaySchedule>
            {
                new DaySchedule(items, DataWeekType.Even, DataDayType.Monday),
                new DaySchedule(items, DataWeekType.Even, DataDayType.Tuesday),
                new DaySchedule(items, DataWeekType.Even, DataDayType.Wednesday),
                new DaySchedule(items, DataWeekType.Even, DataDayType.Thursday),
                new DaySchedule(items, DataWeekType.Even, DataDayType.Friday),
                new DaySchedule(items, DataWeekType.Even, DataDayType.Saturday)
            };
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