using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItmoScheduleApiWrapper;
using ItmoScheduleApiWrapper.Models;

namespace ScheduleAggregator
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            var schedule = GetGroups()
                .AsParallel()
                .Select(GetScheduleItemModels)
                .SelectMany(e => e)
                .Select(e => (e.DataDay, e.DataWeek, e.Group, e.StartTime, e.SubjectTitle))
                .GroupBy(e => e.DataDay)
                .ToList();

            foreach (var tuple in schedule)
            {
                Console.WriteLine($"\t\t{tuple.Key}");
                foreach (var valueTuple in tuple)
                {
                    Console.WriteLine(valueTuple);
                }
            }

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
    }
}
