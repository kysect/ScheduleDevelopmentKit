using System;
using System.Collections.Generic;

namespace ScheduleDevelopmentKit.Domain.Entities
{
    public class SemesterSubject
    {
        public Guid Id { get; set; }

        public Subject Subject { get; set; }
        public LabourIntensity LabourIntensity { get; set; }
        public Semester Semester { get; set; }

        public List<Lesson> Lessons { get; set; }
    }
}