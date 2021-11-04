using System;

namespace ScheduleDevelopmentKit.Domain.Enums
{
    public enum TimeSlot
    {
        Lesson1,
        Lesson2,
        Lesson3,
        Lesson4,
        Lesson5,
        Lesson6,
        Lesson7,
        Lesson8
    }

    public static class TimeSlotExtensionMethods
    {
        public static (string startTime, string endTime) ToTimeLimits(this TimeSlot timeSlot) =>
            timeSlot switch
            {
                TimeSlot.Lesson1 => ("8:20", "9:50"),
                TimeSlot.Lesson2 => ("10:00", "11:30"),
                TimeSlot.Lesson3 => ("11:40", "13:10"),
                TimeSlot.Lesson4 => ("13:30", "15:00"),
                TimeSlot.Lesson5 => ("15:20", "16:50"),
                TimeSlot.Lesson6 => ("17:00", "18:30"),
                TimeSlot.Lesson7 => ("18:40", "20:10"),
                TimeSlot.Lesson8 => ("20:20", "21:50"),
                _ => throw new ArgumentOutOfRangeException(nameof(timeSlot), timeSlot,
                    "Хз, что за пару ты придумал, но у нас их всего 8, клоун")
            };
    }
}