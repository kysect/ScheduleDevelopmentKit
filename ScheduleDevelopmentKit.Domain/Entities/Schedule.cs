using System;
using System.Collections.Generic;

namespace ScheduleDevelopmentKit.Domain.Entities
{
    public class Schedule
    {
        public Guid Id { get; set; }

        public List<Lesson> Lessons { get; set; }
    }
}