using System.Collections.Generic;
using System.Linq;
using Kysect.ItmoScheduleSdk;
using Kysect.ItmoScheduleSdk.Models;

namespace ScheduleAggregator.Core.ScheduleItemProviders
{
    public class ApiScheduleItemProvider : IScheduleItemProvider
    {
        public ApiScheduleItemProvider(IEnumerable<string> groupList, IEnumerable<int> userList)
        {
            GroupList = groupList.ToList();
            UserList = userList.ToList();
        }

        public List<string> GroupList { get; }
        public List<int> UserList { get; }

        private List<ScheduleItemModel> _scheduleItemModels;

        public List<ScheduleItemModel> GetItems()
        {
            if (_scheduleItemModels != null)
            {
                return _scheduleItemModels;
            }

            var provider = new ItmoApiProvider();

            _scheduleItemModels = provider.ScheduleApi
                .GetGroupPackSchedule(GroupList)
                .Concat(provider.ScheduleApi.GetPersonPackSchedule(UserList))
                .ScheduleOrder()
                .ToList();

            return _scheduleItemModels;
        }
    }
}