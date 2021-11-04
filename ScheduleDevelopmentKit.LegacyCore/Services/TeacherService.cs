using System;
using System.Collections.Generic;
using System.Linq;
using ScheduleDevelopmentKit.Domain.Entities;
using ScheduleDevelopmentKit.LegacyCore;
using ScheduleDevelopmentKit.LegacyCore.Interfaces;

namespace ScheduleDevelopmentKit.LegacyCore.Services
{
    public class TeacherService : IService<Teacher>
    {
        private readonly UnitOfWork _uof;

        public TeacherService(UnitOfWork uof)
        {
            _uof = uof;
        }

        public Guid Create(string name)
        {
            if (_uof.Teachers.Get().Any(t => t.Name == name))
                throw new Exception("The Teacher already exists");

            var result = new Teacher() { Name = name };
            _uof.Teachers.Create(result);
            return result.Id;
        }

        public void AddSubject(Guid teacherId, Guid subjectId)
        {
            var teacher = _uof.Teachers.FindById(teacherId);
            var subject = _uof.Subjects.FindById(subjectId);
            if (!teacher.Subjects.Exists(s => s.Id == subject.Id))
            {
                teacher.Subjects.Add(subject);
                _uof.Teachers.Update(teacher);
            }
        }

        public Teacher FindById(Guid id)
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
    }
}
