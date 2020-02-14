using System.Collections.Generic;
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
        public MainWindow()
        {
            InitializeComponent();

            var provider = new ScheduleItemProvider(new List<string> {"M3101", "M3107" }, null);
            var items = provider.GetItemsForGroup();
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
    }
}
