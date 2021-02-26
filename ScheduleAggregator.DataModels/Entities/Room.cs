using System;
using ScheduleAggregator.DataModels.Enums;

namespace ScheduleAggregator.DataModels.Entities
{
    public class Room
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        public Campus Campus { get; set; }
    }
}