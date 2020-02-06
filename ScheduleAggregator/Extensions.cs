using System;
using ItmoScheduleApiWrapper.Models;

namespace ScheduleAggregator
{
    public static class Extensions
    {
        public static Boolean IsLecture(this ScheduleItemModel model)
        {
            return model.Status == "Лек";
        }
    }
}