using System;
using System.Collections.Generic;

namespace ScheduleDevelopmentKit.Domain.Entities
{
    public class StudyStream
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<StudyGroup> Groups { get; set; } = new();
        public List<Lesson> Lessons { get; set; } = new();
    }
}