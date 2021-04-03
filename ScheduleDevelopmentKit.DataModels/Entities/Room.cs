using System;
using ScheduleDevelopmentKit.DataModels.Enums;

namespace ScheduleDevelopmentKit.DataModels.Entities
{
    public class Room
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        public Campus Campus { get; set; }
    }
}