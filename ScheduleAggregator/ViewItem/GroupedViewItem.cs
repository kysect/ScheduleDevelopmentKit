using System.Collections.Generic;
using System.Linq;
using ItmoScheduleApiWrapper.Models;

namespace ScheduleAggregator.ViewItem
{
    public class GroupedViewItem : IScheduleIViewItem
    {
        public List<ScheduleItemModel> Models { get; set; }

        public GroupedViewItem(List<ScheduleItemModel> models)
        {
            Models = models;
        }

        public string ToViewString()
        {
            string groups = string.Join("; ", Models.Select(m => m.Group));
            ScheduleItemModel first = Models[0];
            return (first.StartTime, first.ShortSubjectTitle(), first.ShortTeacherName(), groups).ToString();
        }
    }
}