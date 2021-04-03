using System.Collections.ObjectModel;
using System.Windows.Input;
using ScheduleAggregator.DataModels.Entities;
using ScheduleAggregator.Desktop.Commands;
using ScheduleAggregator.Desktop.Tools;

namespace ScheduleAggregator.Desktop.ViewModels
{
    public class AddCourseViewModel : BaseViewModel
    {
        public AddCourseViewModel()
        {
            AddNewCourseCommand = new BaseCommand(_ => AddNewCourse());
            Courses = new ObservableCollection<StudyCourse>(ExecutionContext.Instance.Courses);
        }

        public ObservableCollection<StudyCourse> Courses { get; }

        public string NewCourseName { get; set; }

        public ICommand AddNewCourseCommand { get; }

        private void AddNewCourse()
        {
            var newCourse = new StudyCourse {Name = NewCourseName};
            ExecutionContext.Instance.Courses.Add(newCourse);
            Courses.Add(newCourse);
            OnPropertyChanged(nameof(Courses));
        }
    }
}