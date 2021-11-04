using System;
using System.Collections.Generic;

namespace ScheduleDevelopmentKit.Domain.Entities
{
    public class Subject
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public List<Teacher> Teachers { get; set; }

        public List<SemesterSubject> SemesterSubjects { get; set; }
    }
}