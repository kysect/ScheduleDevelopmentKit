using System;

namespace ScheduleDevelopmentKit.DataModels.Enums
{
    public enum Campus
    {
        Undefined,
        Lomonosova,
        Kronverskiy,
        Birjevaya
    }

    public static class CampusExtensionsMethods
    {
        public static string ToRuString(this Campus campus) =>
            campus switch
            {
                Campus.Undefined => "Неизвестно",
                Campus.Lomonosova => "улица Ломоносова, д. 9",
                Campus.Kronverskiy => "Кронверкский проспект, д. 49",
                Campus.Birjevaya => "Биржевая линия, д. 14",
                _ => throw new ArgumentOutOfRangeException(nameof(campus), campus, null)
            };
    }
}