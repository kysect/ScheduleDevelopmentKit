using ScheduleAggregator.DataModels.Entities;
using ScheduleAggregator.DataModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleAggregator.DataModels.Services
{
    public class SubjectService
    {
        private UnitOfWork _uof;
        public SubjectService(UnitOfWork uof)
        {
            _uof = uof;
        }
        public void Create(string name)
        {
            if (_uof.Subjects.Get(_ => _.Name == name) != null)
                throw new Exception("The Subject already exists");

            _uof.Subjects.Create(new Subject() { Name = name });
        }
        public void Update(Subject subject)
        {
            _uof.Subjects.Update(subject);
        }

        public void AddTeacher(Subject subject, Teacher teacher)
        {
            if (!subject.Teachers.Exists(_ => _.Id == teacher.Id))
            {
                subject.Teachers.Add(teacher);
                _uof.Subjects.Update(subject);
            }
        }

        public void AddSemesterSubject(Subject subject, SemesterSubject semesterSubject)
        {
            if (!subject.SemesterSubjects.Exists(_ => _.Id == semesterSubject.Id))
            {
                subject.SemesterSubjects.Add(semesterSubject);
                _uof.Subjects.Update(subject);
            }
        }

        ///

        public Subject FindByID(int id)
        {
            return _uof.Subjects.FindById(id);
        }

        public IEnumerable<Subject> Get()
        {
            return _uof.Subjects.Get();
        }
        public void Remove(Subject subject)
        {
            _uof.Subjects.Remove(subject);
        }
    }
}
