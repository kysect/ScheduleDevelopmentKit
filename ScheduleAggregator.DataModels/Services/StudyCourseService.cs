using ScheduleAggregator.DataModels.Entities;
using ScheduleAggregator.DataModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleAggregator.DataModels.Services
{
    public class StudyCourseService
    {
        private UnitOfWork _uof;
        public StudyCourseService(UnitOfWork uof)
        {
            _uof = uof;
        }
        public void Create(string name)
        {
            if (_uof.StudyCourses.Get(_ => _.Name == name) != null)
                throw new Exception("The StudyCource already exists");

            _uof.StudyCourses.Create(new StudyCourse() { Name = name });
        }
        public void Update(StudyCourse studyCourse)
        {
            _uof.StudyCourses.Update(studyCourse);
        }

        public void AddGroup(StudyCourse course, StudyGroup group)
        {
            if (!course.Groups.Exists(_ => _.Id == group.Id))
            {
                course.Groups.Add(group);
                _uof.StudyCourses.Update(course);
            }
        }
        public void AddSemester(StudyCourse course, Semester semester)
        {
            if (!course.Semesters.Exists(_ => _.Id == semester.Id))
            {
                course.Semesters.Add(semester);
                _uof.StudyCourses.Update(course);
            }
        }


        ///

        public StudyCourse FindByID(int id)
        {
            return _uof.StudyCourses.FindById(id);
        }

        public IEnumerable<StudyCourse> Get()
        {
            return _uof.StudyCourses.Get();
        }
        public void Remove(StudyCourse studyCourse)
        {
            _uof.StudyCourses.Remove(studyCourse);
        }

    }
}
