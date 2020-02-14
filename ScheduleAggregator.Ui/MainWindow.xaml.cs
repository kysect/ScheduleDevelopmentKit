using System.Collections.Generic;
using System.Windows;
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
            var provider = new ScheduleItemProvider(new List<string>() {"M3101"}, null);
            List<DaySchedule> schedule = new List<DaySchedule>();
            schedule.Add(new DaySchedule(provider.GetItemsForGroup(), DataWeekType.Odd, DataDayType.Monday));
            OddItemList.ItemsSource = schedule;

        }
    }
}
