using System;

namespace ScheduleDevelopmentKit.DataModels.Enums
{
    public enum DaySlot
    {
        Monday,
        Tuesday,
        Wednesday, 
        Thursday, 
        Friday, 
        Saturday
    }
    
    public static class DaySlotExtensionMethods
    {
        public static string ToRuString(this DaySlot daySlot) =>
            daySlot switch
            {
                DaySlot.Monday => "Понедельник",
                DaySlot.Tuesday => "Вторник",
                DaySlot.Wednesday => "Среда",
                DaySlot.Thursday => "Четверг",
                DaySlot.Friday => "Пятница",
                DaySlot.Saturday => "Cуббота",
                _ => throw new ArgumentOutOfRangeException(nameof(daySlot),
                    daySlot,
                    "Хз, что за день недели ты придумал, но у нас шестидневка, клоун")
            };
        
        public static string ToShortRuString(this DaySlot daySlot) =>
            daySlot switch
            {
                DaySlot.Monday => "Пн",
                DaySlot.Tuesday => "Вт",
                DaySlot.Wednesday => "Ср",
                DaySlot.Thursday => "Чт",
                DaySlot.Friday => "Пт",
                DaySlot.Saturday => "Сб",
                _ => throw new ArgumentOutOfRangeException(nameof(daySlot),
                    daySlot,
                    "Хз, что за день недели ты придумал, но у нас шестидневка, клоун")
            };
    }
}