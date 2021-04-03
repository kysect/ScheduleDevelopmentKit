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
            Courses = new ObservableCollection<StudyCourse>(ExecutionContext.Instance.StudyCourseService.Get());
        }

        public ObservableCollection<StudyCourse> Courses { get; }

        public string NewCourseName { get; set; }

        public ICommand AddNewCourseCommand { get; }

        private void AddNewCourse()
        {
            StudyCourse studyCourse = ExecutionContext.Instance.StudyCourseService.Create(NewCourseName);
            Courses.Add(studyCourse);
            OnPropertyChanged(nameof(Courses));
        }
    }
}