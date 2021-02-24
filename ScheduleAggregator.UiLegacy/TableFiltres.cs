using Kysect.ItmoScheduleSdk.Models;
using Kysect.ItmoScheduleSdk.Types;
using ScheduleAggregator.Ui.CustomElements;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleAggregator.Ui
{
    public class TableFiltres
    {
        public DataWeekType WeekType = DataWeekType.Even;
        public string StartTime = default;
        public string Room = default;
        public string Teacher = default;
        public string SubjectTitle = default;

        public List<DaySchedule> Filter(List<DayScheduleDescriptor> descriptors)
        {

            var Shedule = new List<DayScheduleDescriptor>();
            foreach (var day in descriptors.Where(d => d.DataWeek == WeekType).ToList())
            {

                IEnumerable<ScheduleItemModel> MaskedScheduleItems = day.ScheduleItems;

                if (Room != default)
                    MaskedScheduleItems = MaskedScheduleItems.Where(l => l.Room == Room);
                if (StartTime != default)
                    MaskedScheduleItems = MaskedScheduleItems.Where(l => l.StartTime == StartTime);
                if (SubjectTitle != default)
                    MaskedScheduleItems = MaskedScheduleItems.Where(l => l.SubjectTime == SubjectTitle);
                if (Teacher != default)
                    MaskedScheduleItems = MaskedScheduleItems.Where(l => l.Teacher == Teacher);

                Shedule.Add(new DayScheduleDescriptor(day.DataDay, day.DataWeek, MaskedScheduleItems.ToList()));

            }
            return Shedule.Select(d => new DaySchedule(d)).ToList();
        }
    }
}
