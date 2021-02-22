using System;

namespace ScheduleAggregator.DataModels.Enums
{
    public enum Campus
    {
        Lomonosova,
        Kronverskiy,
        Birjevaya,
        Undefined
    }

    public static class CampusExtensionsMethods
    {
        public static string ToRuString(this Campus campus) =>
            campus switch
            {
                Campus.Lomonosova => "улица Ломоносова, д. 9",
                Campus.Kronverskiy => "Кронверкский проспект, д. 49",
                Campus.Birjevaya => "Биржевая линия, д. 14",
                Campus.Undefined => "Неизвестно",
                _ => throw new ArgumentOutOfRangeException(nameof(campus), campus, null)
            };
    }
    
}