using System.Collections.Generic;
using Kysect.ItmoScheduleSdk.Models;

namespace ScheduleDevelopmentKit.Core.ScheduleItemProviders
{
    public interface IScheduleItemProvider
    {
        List<ScheduleItemModel> GetItems();
    }
}