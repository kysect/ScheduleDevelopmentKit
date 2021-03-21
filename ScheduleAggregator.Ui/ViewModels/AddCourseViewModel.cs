using System.Collections.ObjectModel;
using System.Windows.Input;
using ScheduleAggregator.DataModels.Entities;
using ScheduleAggregator.Ui.Commands;
using ScheduleAggregator.Ui.Tools;

namespace ScheduleAggregator.Ui.ViewModels
{
    public class AddCourseViewModel : BaseViewModel
    {
        public AddCourseViewModel()
        {
            AddNewCourseCommand = new BaseCommand(_ => AddNewCourse());
            Courses = new ObservableCollection<StudyCourse>(ExecutionContext.Instance.StudyCourseService.Get());
        }

        public ObservableCollection<StudyCourse> Courses { get; }

        public string NewCourseName { get; set; }

        public ICommand AddNewCourseCommand { get; }

        private void AddNewCourse()
        {
            var newCourseID = ExecutionContext.Instance.StudyCourseService.Create(NewCourseName);
            var newCourse = ExecutionContext.Instance.StudyCourseService.FindById(newCourseID);
            Courses.Add(newCourse);
            OnPropertyChanged(nameof(Courses));
        }
    }
}