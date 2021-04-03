using System;

namespace ScheduleDevelopmentKit.DataModels.Enums
{
    public enum LessonType
    {
        Lecture,
        Laboratory,
        Practise
    }

    public static class ClassTypeExtensionMethods
    {
        public static string ToRuString(this LessonType lessonType) =>
            lessonType switch
            {
                LessonType.Lecture => "Лекционное занятие",
                LessonType.Laboratory => "Лабораторное занятие",
                LessonType.Practise => "Практическое занятие",
                _ => throw new ArgumentOutOfRangeException(nameof(lessonType), lessonType, null)
            };
        
        public static string ToShortRuString(this LessonType lessonType) =>
            lessonType switch
            {
                LessonType.Lecture => "Лк",
                LessonType.Laboratory => "Лб",
                LessonType.Practise => "Пр",
                _ => throw new ArgumentOutOfRangeException(nameof(lessonType), lessonType, null)
            };
    }
}