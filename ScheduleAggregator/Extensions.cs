using ItmoScheduleApiWrapper.Models;

namespace ScheduleAggregator
{
    public static class Extensions
    {
        public static bool IsLecture(this ScheduleItemModel model)
        {
            return model.Status == "Лек";
        }
    }
}