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
        public Guid Create(string name)
        {
            if (_uof.Subjects.Get(el => el.Name == name).Any())
                throw new Exception("The Subject already exists");

            var Out = new Subject() { Name = name };
            _uof.Subjects.Create(Out);
            return Out.Id;
        }
        public void AddTeacher(Guid subjectID, Guid teacherID)
        {
            var subject = _uof.Subjects.FindById(subjectID);
            if (!subject.Teachers.Exists(el => el.Id == teacherID))
            {
                subject.Teachers.Add(_uof.Teachers.FindById(teacherID));
                _uof.Subjects.Update(subject);
            }
        }

        public void AddSemesterSubject(Guid subjectID, Guid semesterSubjectID)
        {
            var subject = _uof.Subjects.FindById(subjectID);
            var semesterSubject = _uof.SemesterSubjects.FindById(semesterSubjectID);
            if (!subject.SemesterSubjects.Exists(el => el.Id == semesterSubject.Id))
            {
                subject.SemesterSubjects.Add(semesterSubject);
                _uof.Subjects.Update(subject);
            }
        }

        #region SameOperations

        public Subject FindByID(Guid id)
        {
            return _uof.Subjects.FindById(id);
        }

        public IEnumerable<Subject> Get()
        {
            return _uof.Subjects.Get();
        }

        public IEnumerable<Subject> Get(Func<Subject, bool> predicate)
        {
            return _uof.Subjects.Get(predicate);
        }
        public void Remove(Guid subjectID)
        {
            _uof.Subjects.Remove(_uof.Subjects.FindById(subjectID));
        }

        #endregion
    }
}
