using System;
using System.Collections.Generic;

namespace ScheduleAggregator.DataModels.Entities
{
    public class Semester
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public List<SemesterSubject> Subjects { get; set; }

        public StudyCourse StudyCourse { get; set; }
    }
}