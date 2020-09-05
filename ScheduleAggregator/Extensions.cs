using ItmoScheduleApiWrapper.Models;

namespace ScheduleAggregator
{
    public static class Extensions
    {
        public static bool IsLecture(this ScheduleItemModel model)
        {
            return model.Status == "Лек";
        }

        public static string ShortTeacherName(this ScheduleItemModel model)
        {
            if (string.IsNullOrEmpty(model.Teacher))
                return model.Teacher;

            string[] parts = model.Teacher.Split(" ");
            for (var i = 1; i < parts.Length; i++)
            {
                parts[i] = $"{parts[i][0]}.";
            }

            return string.Join(" ", parts);
        }
    }
}