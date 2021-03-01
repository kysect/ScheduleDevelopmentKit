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
        public Teacher Create(string name)
        {
            if (_uof.Teachers.Get(_ => _.Name == name) != null)
                throw new Exception("The Teacher already exists");

            var Out = new Teacher() { Name = name };
            _uof.Teachers.Create(Out);
            return Out;
        }
        public void Update(Teacher teacher)
        {
            _uof.Teachers.Update(teacher);
        }

        public void AddSubject(Teacher teacher, Subject subject)
        {
            if (!teacher.Subjects.Exists(_ => _.Id == subject.Id))
            {
                teacher.Subjects.Add(subject);
                _uof.Teachers.Update(teacher);
            }
        }

        public void AddLessons(Teacher teacher, Lesson lesson)
        {
            if (!teacher.Lessons.Exists(_ => _.Id == lesson.Id))
            {
                teacher.Lessons.Add(lesson);
                _uof.Teachers.Update(teacher);
            }
        }

        #region SameOperations

        public Teacher FindByID(int id)
        {
            return _uof.Teachers.FindById(id);
        }

        public IEnumerable<Teacher> Get()
        {
            return _uof.Teachers.Get();
        }
        public void Remove(Teacher item)
        {
            _uof.Teachers.Remove(item);
        }
        #endregion  
    }
}
