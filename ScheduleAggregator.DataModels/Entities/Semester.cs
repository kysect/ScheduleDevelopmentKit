using System;
using System.Collections.Generic;
using System.Dynamic;

namespace ScheduleAggregator.DataModels.Entities
{
    public class Semester
    {
        public Guid Id { get; set; }

        public string Name;
        public List<SemesterSubject> Subjects { get; set; }

        public StudyCourse StudyCourse { get; set; }
    }
}