using System;
using System.Collections.Generic;

namespace ScheduleDevelopmentKit.Domain.Entities
{
    public class Teacher
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public List<Subject> Subjects { get; set; }

        public List<Lesson> Lessons { get; set; }
    }
}