using ScheduleAggregator.DataModels.Entities;
using ScheduleAggregator.DataModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleAggregator.DataModels.Services
{
    public class TeacherService
    {
        private UnitOfWork _uof;
        public TeacherService(UnitOfWork uof)
        {
            _uof = uof;
        }
        public Guid Create(string name)
        {
            if (_uof.Teachers.Get(_ => _.Name == name) != null)
                throw new Exception("The Teacher already exists");

            var Out = new Teacher() { Name = name };
            _uof.Teachers.Create(Out);
            return Out.Id;
        }
        public void Update(Teacher teacher)
        {
            _uof.Teachers.Update(teacher);
        }

        public void AddSubject(Guid teacherID, Guid subjectID)
        {
            var teacher = _uof.Teachers.FindById(teacherID);
            var subject = _uof.Subjects.FindById(subjectID);
            if (!teacher.Subjects.Exists(_ => _.Id == subject.Id))
            {
                teacher.Subjects.Add(subject);
                _uof.Teachers.Update(teacher);
            }
        }

        public void AddLessons(Guid teacherID, Guid lessonID)
        {
            var teacher = _uof.Teachers.FindById(teacherID);
            var lesson = _uof.Lessons.FindById(lessonID);
            if (!teacher.Lessons.Exists(_ => _.Id == lesson.Id))
            {
                teacher.Lessons.Add(lesson);
                _uof.Teachers.Update(teacher);
            }
        }

        #region SameOperations

        public Teacher FindByID(Guid id)
        {
            return _uof.Teachers.FindById(id);
        }

        public IEnumerable<Teacher> Get()
        {
            return _uof.Teachers.Get();
        }
        public void Remove(Guid teacherID)
        {
            _uof.Teachers.Remove(_uof.Teachers.FindById(teacherID));
        }
        #endregion  
    }
}
