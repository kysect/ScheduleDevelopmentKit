using System;
using System.Collections.Generic;

namespace ScheduleAggregator.DataModels.Entities
{
    public class Schedule
    {
        public Guid Id { get; set; }

        public List<Lesson> Lessons { get; set; }
    }
}