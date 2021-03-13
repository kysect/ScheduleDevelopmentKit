using ScheduleAggregator.DataModels.Entities;
using ScheduleAggregator.DataModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

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
            if (_uof.Teachers.Get().Any(el => el.Name == name))
                throw new Exception("The Teacher already exists");

            var Out = new Teacher() { Name = name };
            _uof.Teachers.Create(Out);
            return Out.Id;
        }
        public void AddSubject(Guid teacherID, Guid subjectID)
        {
            var teacher = _uof.Teachers.FindById(teacherID);
            var subject = _uof.Subjects.FindById(subjectID);
            if (!teacher.Subjects.Exists(el => el.Id == subject.Id))
            {
                teacher.Subjects.Add(subject);
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
        
        public void Remove(Guid teacherId)
        {
            _uof.Teachers.Remove(_uof.Teachers.FindById(teacherId));
        }

        public void Update(Teacher teacher)
        {
            _uof.Teachers.Update(teacher);
        }
        #endregion  
    }
}
