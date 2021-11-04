using System.Collections.Generic;
using Kysect.ItmoScheduleSdk.Models;

namespace ScheduleDevelopmentKit.ConsolePolygon.ScheduleItemProviders
{
    public interface IScheduleItemProvider
    {
        List<ScheduleItemModel> GetItems();
    }
}