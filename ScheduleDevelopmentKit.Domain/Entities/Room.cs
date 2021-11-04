using System;
using ScheduleDevelopmentKit.Domain.Enums;

namespace ScheduleDevelopmentKit.Domain.Entities
{
    public class Room
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public Campus Campus { get; set; }
    }
}