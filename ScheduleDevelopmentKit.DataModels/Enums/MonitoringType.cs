using System;

namespace ScheduleDevelopmentKit.DataModels.Enums
{
    public enum MonitoringType
    {
        Zachet,
        DiffZachet,
        Exam
    }

    public static class MonitoringTypeExtensionMethods
    {
        public static string ToRuString(this MonitoringType monitoringType) =>
            monitoringType switch
            {
                MonitoringType.Zachet => "Зачет",
                MonitoringType.DiffZachet => "Дифф зачет",
                MonitoringType.Exam => "Экзамен",
                _ => throw new ArgumentOutOfRangeException(nameof(monitoringType), monitoringType, null)
            };
    }
}