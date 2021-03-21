using System.Collections.Generic;
using ScheduleAggregator.DataModels.Entities;

namespace ScheduleAggregator.Ui.Tools
{
    public class ExecutionContext
    {
        public static ExecutionContext Instance = new ExecutionContext();

        public ExecutionContext()
        {
            Courses = new List<StudyCourse>();
        }

        public List<StudyCourse> Courses { get; set; }
    }
}