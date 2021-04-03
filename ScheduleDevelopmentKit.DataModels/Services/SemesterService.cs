using ScheduleDevelopmentKit.DataModels.Entities;
using ScheduleDevelopmentKit.DataModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using ScheduleDevelopmentKit.DataModels.Interfaces;

namespace ScheduleDevelopmentKit.DataModels.Services
{
    public class SemesterService : IService<Semester>
    {
        private readonly UnitOfWork _uof;

        public SemesterService(UnitOfWork uof)
        {
            _uof = uof;
        }

        public Guid Create(string name, Guid courseId)
        {
            if (_uof.Semesters.Get().Any(s => s.Name == name))
                throw new Exception("The Semester already exists");

            var result = new Semester() { Name = name, StudyCourse = _uof.StudyCourses.FindById(courseId)};

            _uof.Semesters.Create(result);
            return result.Id;
        }

        public void AddSubject(Guid semesterId, Guid subjectId)
        {
            var semester = _uof.Semesters.FindById(semesterId);
            if (!semester.Subjects.Exists(s => s.Id == subjectId))
            {
                semester.Subjects.Add(_uof.SemesterSubjects.FindById(subjectId));
                _uof.Semesters.Update(semester);
            }
        }

        public Semester FindById(Guid id)
        {
            return _uof.Semesters.FindById(id);
        }

        public IEnumerable<Semester> Get()
        {
            return _uof.Semesters.Get();
        }

        public void Remove(Guid semesterId)
        {
            _uof.Semesters.Remove(_uof.Semesters.FindById(semesterId));
        }

        public void Update(Semester semester)
        {
            _uof.Semesters.Update(semester);
        }
    }
}
