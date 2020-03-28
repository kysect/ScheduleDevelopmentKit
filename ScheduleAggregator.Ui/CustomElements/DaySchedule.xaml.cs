using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using ItmoScheduleApiWrapper.Helpers;
using ItmoScheduleApiWrapper.Models;
using ItmoScheduleApiWrapper.Types;

namespace ScheduleAggregator.Ui.CustomElements
{
    public partial class DaySchedule : UserControl
    {
        public DaySchedule(List<ScheduleItemModel> items, DataWeekType dataWeekType, DataDayType dataDayType)
        {
            List<ScheduleItemModel> currentItems = items
                .Where(i => i.DataWeek.Compare(dataWeekType))
                .Where(i => i.DataDay == dataDayType)
                .ToList();

            var printableData =
                currentItems
                    .Select(e => (e.StartTime, e.SubjectTitle, e.Status, e.Group, e.Teacher))
                    .Select(t => t.ToString())
                    .ToList();

            InitializeComponent();
            ItemsList.ItemsSource = printableData;
        }
    }
}
