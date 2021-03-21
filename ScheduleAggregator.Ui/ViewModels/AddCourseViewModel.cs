using System.Collections.Generic;
using ScheduleAggregator.DataModels.Entities;
using ScheduleAggregator.Ui.Tools;

namespace ScheduleAggregator.Ui.ViewModels
{
    public class AddCourseViewModel
    {
        public List<StudyCourse> Courses => ExecutionContext.Instance.Courses;


    }
}