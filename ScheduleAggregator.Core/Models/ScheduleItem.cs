namespace ScheduleAggregator.Core.Models
{
    public class ScheduleItem
    {
        public StudyGroup StudyGroup { get; set; }
        public Room Room { get; set; }
        public Teacher Teacher { get; set; }
        //TODO: add time
    }
}