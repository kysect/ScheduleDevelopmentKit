using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItmoScheduleApiWrapper;
using ItmoScheduleApiWrapper.Models;
using MoreLinq;

namespace ScheduleAggregator
{
    static class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            var scheduleItems = GetGroups()
                .AsParallel()
                .Select(GetScheduleItemModels)
                .SelectMany(e => e)
                .ToList();

            Console.WriteLine("\t\tLectures");
            Print(scheduleItems
                .Where(e => e.IsLecture())
                .DistinctBy(e => (e.StartTime, e.SubjectTitle, e.Room))
                .ToList());

            Console.WriteLine("\n\n\t\tPractice");
            Print(scheduleItems
                .Where(e => e.IsLecture() == false)
                .ToList());
        }

        public static IEnumerable<ScheduleItemModel> GetScheduleItemModels(String groupTitle)
        {
            ItmoApiProvider provider = new ItmoApiProvider();

            var result = provider
                .ScheduleApi
                .GetGroupSchedule(groupTitle)
                .Result
                .Schedule;
            result.ForEach(s => s.Group = groupTitle);
            return result;
        }

        private static IEnumerable<String> GetGroups()
        {
            yield return "M3403";
            yield return "M3404";
            yield return "M3405";
            yield return "M3406";
            yield return "M3407";
            yield return "M3408";
            yield return "M3409";
        }

        private static Boolean IsLecture(this ScheduleItemModel model)
        {
            return model.Status == "Лек";
        }

        private static void Print(List<ScheduleItemModel> items)
        {
            var scheduleItems = items
                .Select(e => (e.DataDay, e.DataWeek, e.Group, e.StartTime, e.SubjectTitle))
                .GroupBy(e => (e.DataDay, e.DataWeek))
                .ToList();

            foreach (var tuple in scheduleItems)
            {
                Console.WriteLine($"\t\t{tuple.Key}");
                foreach (var valueTuple in tuple)
                {
                    Console.WriteLine(valueTuple);
                }
            }
        }
    }
}
