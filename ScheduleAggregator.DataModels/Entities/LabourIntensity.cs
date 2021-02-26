using System;
using ScheduleAggregator.DataModels.Enums;

namespace ScheduleAggregator.DataModels.Entities
{
    public class LabourIntensity
    {
        public Guid Id { get; set; }
        
        public uint Lecture { get; set; }
        public uint Laboratory { get; set; }
        public uint Practise { get; set; }

        public uint Total => Lecture + Laboratory + Practise;

        public uint GetByLessonType(LessonType lessonType) =>
            lessonType switch
            {
                LessonType.Lecture => Lecture,
                LessonType.Laboratory => Laboratory,
                LessonType.Practise => Practise,
                _ => throw new ArgumentOutOfRangeException(nameof(lessonType), lessonType, null)
            };
    }
}