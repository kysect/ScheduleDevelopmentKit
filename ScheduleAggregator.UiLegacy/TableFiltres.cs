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

        public List<DaySchedule> PutMasks(List<DayScheduleDescriptor> descriptors)
        {
            if (descriptors == null)
                return null;

            var Shedule = new List<DayScheduleDescriptor>();

            foreach (var day in descriptors.Where(d => d.DataWeek == WeekType).ToList())
            {

                var MaskedScheduleItems = day.ScheduleItems;

                if (Room != default)
                    MaskedScheduleItems = MaskedScheduleItems.Where(l => l.Room == Room).ToList();
                if (StartTime != default)
                    MaskedScheduleItems = MaskedScheduleItems.Where(l => l.StartTime == StartTime).ToList();
                if (SubjectTitle != default)
                    MaskedScheduleItems = MaskedScheduleItems.Where(l => l.SubjectTime == SubjectTitle).ToList();
                if (Teacher != default)
                    MaskedScheduleItems = MaskedScheduleItems.Where(l => l.Teacher == Teacher).ToList();

                Shedule.Add(new DayScheduleDescriptor(day.DataDay, day.DataWeek, MaskedScheduleItems));

            }
            return Shedule.Select(d => new DaySchedule(d)).ToList();
        }
    }
}
