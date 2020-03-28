using System.Collections.Generic;
using System.Linq;
using ItmoScheduleApiWrapper;
using ItmoScheduleApiWrapper.Models;
using ScheduleAggregator.Printers;

namespace ScheduleAggregator
{
    public class ScheduleItemProvider
    {
        public ScheduleItemProvider(IEnumerable<string> groupList, IEnumerable<int> userList, IPrinter printer)
        {
            GroupList = groupList.ToList();
            UserList = userList.ToList();
            _printer = printer;
        }

        public List<string> GroupList { get; }
        public List<int> UserList { get; }

        private List<ScheduleItemModel> _scheduleItemModels;
        private readonly IPrinter _printer;

        public void PrintLecture()
        {
            Print(GetItemsForGroup().Where(e => e.IsLecture()));
        }

        public void PrintPractice()
        {
            Print(GetItemsForGroup().Where(e => !e.IsLecture()));
        }

        public List<ScheduleItemModel> GetItemsForGroup()
        {
            if (_scheduleItemModels != null)
            {
                return _scheduleItemModels;
            }

            var provider = new ItmoApiProvider();

            IEnumerable<ScheduleItemModel> GetGroupSchedule(string groupTitle) =>
                provider
                    .ScheduleApi
                    .GetGroupSchedule(groupTitle)
                    .Result
                    .Schedule;

            IEnumerable<ScheduleItemModel> GetPersonSchedule(int userId) =>
                provider
                    .ScheduleApi
                    .GetPersonSchedule(userId)
                    .Result
                    .Schedule;

            List<ScheduleItemModel> groupsItems = GroupList
                .AsParallel()
                .Select(GetGroupSchedule)
                .SelectMany(e => e)
                .ToList();

            List<ScheduleItemModel> usersItems = UserList
                .AsParallel()
                .Select(GetPersonSchedule)
                .SelectMany(e => e)
                .ToList();

            _scheduleItemModels = groupsItems
                .Concat(usersItems)
                .OrderBy(e => e.StartTime)
                .ThenBy(e => e.SubjectTitle)
                .ThenBy(e => e.Group)
                .ThenBy(e => e.Teacher)
                .ToList();

            return _scheduleItemModels;
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