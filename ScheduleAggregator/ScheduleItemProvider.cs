using System.Collections.Generic;
using System.Linq;
using ItmoScheduleApiWrapper;
using ItmoScheduleApiWrapper.Models;
using ScheduleAggregator.Printers;

namespace ScheduleAggregator
{
    public class ScheduleItemProvider
    {
        public ScheduleItemProvider(IEnumerable<string> groupList, IPrinter printer)
        {
            GroupList = groupList.ToList();
            _printer = printer;
        }

        public List<string> GroupList { get; }

        private List<ScheduleItemModel> _scheduleItemModels;
        private readonly IPrinter _printer;

        public void PrintLecture()
        {
            Print(GetItemsForGroup(new List<int>()).Where(e => e.IsLecture()));
        }

        public void PrintPractice()
        {
            Print(GetItemsForGroup(new List<int>()).Where(e => !e.IsLecture()));
        }

        public List<ScheduleItemModel> GetItemsForGroup(List<int> userIdList)
        {
            List<ScheduleItemModel> groupsItems = GroupList
                .AsParallel()
                .Select(GetScheduleItemModels)
                .SelectMany(e => e)
                .ToList();

            foreach (int userId in userIdList)
            {
                var provider = new ItmoApiProvider();

                List<ScheduleItemModel> result = provider
                    .ScheduleApi
                    .GetPersonSchedule(userId)
                    .Result
                    .Schedule;

                groupsItems.AddRange(result);
            }

            if (_scheduleItemModels == null)
            {
                _scheduleItemModels = groupsItems
                    .OrderBy(e => e.StartTime)
                    .ThenBy(e => e.SubjectTitle)
                    .ThenBy(e => e.Group)
                    .ThenBy(e => e.Teacher)
                    .ToList();
            }

            return _scheduleItemModels;
        }

        private static IEnumerable<ScheduleItemModel> GetScheduleItemModels(string groupTitle)
        {
            var provider = new ItmoApiProvider();

            List<ScheduleItemModel> result = provider
                .ScheduleApi
                .GetGroupSchedule(groupTitle)
                .Result
                .Schedule;

            return result;
        }

        private void Print(IEnumerable<ScheduleItemModel> items)
        {
            var scheduleItems = items
                .Select(e => (e.DataDay, e.DataWeek, e.Group, e.StartTime, e.SubjectTitle, e.Teacher))
                .GroupBy(e => (e.DataDay, e.DataWeek))
                .ToList();

            foreach (var tuple in scheduleItems)
            {
                _printer.Print($"\t\t{tuple.Key}");
                foreach (var valueTuple in tuple)
                {
                    _printer.Print(valueTuple.ToString());
                }
            }
        }
    }
}