using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using ItmoScheduleApiWrapper.Helpers;
using ItmoScheduleApiWrapper.Models;
using ItmoScheduleApiWrapper.Types;
using ScheduleAggregator.ViewItem;

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
                    .GroupBy(i => (i.StartTime, i.SubjectTitle, i.Teacher))
                    .Select(ToItem)
                    .Select(item => item.ToViewString());

            InitializeComponent();
            ItemsList.ItemsSource = printableData;
        }

        private IScheduleIViewItem ToItem(IEnumerable<ScheduleItemModel> items)
        {
            var list = items.ToList();
            if (list.Count == 1)
                return new SimpleViewItem(list[0]);
            return new GroupedViewItem(list);
        }
    }
}
