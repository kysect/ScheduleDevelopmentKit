using System;

namespace ScheduleDevelopmentKit.DataModels.Entities
{
    public class StudyGroup
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public StudyCourse StudyCourse { get; set; }
    }
}