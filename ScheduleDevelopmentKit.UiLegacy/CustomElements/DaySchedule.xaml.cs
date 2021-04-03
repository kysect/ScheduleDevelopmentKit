using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Kysect.ItmoScheduleSdk.Models;
using GroupedViewItem = ScheduleDevelopmentKit.Core.ViewItem.GroupedViewItem;
using IScheduleIViewItem = ScheduleDevelopmentKit.Core.ViewItem.IScheduleIViewItem;
using SimpleViewItem = ScheduleDevelopmentKit.Core.ViewItem.SimpleViewItem;

namespace ScheduleDevelopmentKit.Ui.CustomElements
{
    public partial class DaySchedule : UserControl
    {
        public DaySchedule(DayScheduleDescriptor dayScheduleDescriptor)
        {
            IEnumerable<string> printableData =
                dayScheduleDescriptor
                    .ScheduleItems
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
