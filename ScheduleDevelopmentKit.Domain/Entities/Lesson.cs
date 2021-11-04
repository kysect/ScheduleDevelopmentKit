using System;
using ScheduleDevelopmentKit.Domain.Enums;

namespace ScheduleDevelopmentKit.Domain.Entities
{
    public class Lesson
    {
        public Guid Id { get; set; }

        public SemesterSubject Subject { get; set; }
        public LessonType LessonType { get; set; }
        public Teacher Teacher { get; set; }

        public Room Room { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public DaySlot DaySlot { get; set; }
        public WeekType WeekType { get; set; }

        public StudyGroup Group { get; set; }
    }
}