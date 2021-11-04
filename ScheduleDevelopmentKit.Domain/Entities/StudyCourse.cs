using System;
using System.Collections.Generic;

namespace ScheduleDevelopmentKit.Domain.Entities
{
    public class StudyCourse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<StudyGroup> Groups { get; set; }
        public List<Semester> Semesters { get; set; }
    }
}