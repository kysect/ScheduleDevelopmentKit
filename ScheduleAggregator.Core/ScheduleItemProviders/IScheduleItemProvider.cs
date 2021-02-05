using System.Collections.Generic;
using Kysect.ItmoScheduleSdk.Models;

namespace ScheduleAggregator.Core.ScheduleItemProviders
{
    public interface IScheduleItemProvider
    {
        List<ScheduleItemModel> GetItems();
    }
}