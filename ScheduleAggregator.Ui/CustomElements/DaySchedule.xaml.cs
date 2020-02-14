using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using ItmoScheduleApiWrapper.Helpers;
using ItmoScheduleApiWrapper.Models;
using ItmoScheduleApiWrapper.Types;

namespace ScheduleAggregator.Ui.CustomElements
{
    /// <summary>
    /// Interaction logic for DaySchedule.xaml
    /// </summary>
    public partial class DaySchedule : UserControl
    {
        private List<ScheduleItemModel> _items;
        public DaySchedule(List<ScheduleItemModel> items, DataWeekType dataWeekType, DataDayType dataDayType)
        {
            _items = items
                .Where(i => i.DataWeek.Compare(dataWeekType))
                .Where(i => i.DataDay == dataDayType)
                .ToList();

            var printableData =
                _items
                    .Select(e => (e.StartTime, e.SubjectTitle, e.Status, e.Group, e.Teacher))
                    .Select(t => t.ToString())
                    .ToList();

            InitializeComponent();
            ItemsList.ItemsSource = printableData;
        }
    }
}
