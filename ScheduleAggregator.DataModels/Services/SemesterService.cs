using ScheduleAggregator.DataModels.Entities;
using ScheduleAggregator.DataModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleAggregator.DataModels.Services
{
    public class SemesterService
    {
        private UnitOfWork _uof;
        public SemesterService(UnitOfWork uof)
        {
            _uof = uof;
        }
        public Guid Create(string name, Guid courseID)
        {
            if (_uof.Semesters.Get(_ => _.Name == name) != null)
                throw new Exception("The Semester already exists");

            var Out = new Semester() { Name = name, StudyCourse = _uof.StudyCourses.FindById(courseID)};

            _uof.Semesters.Create(Out);
            return Out.Id;
        }
        public void Update(Semester semester)
        {
            _uof.Semesters.Update(semester);
        }

        public void AddSubject(Guid semesterID, Guid subjectID)
        {
            var semester = _uof.Semesters.FindById(semesterID);
            if (!semester.Subjects.Exists(_ => _.Id == subjectID))
            {
                semester.Subjects.Add(_uof.SemesterSubjects.FindById(subjectID));
                _uof.Semesters.Update(semester);
            }
        }

        #region SameOperations

        public Semester FindByID(Guid id)
        {
            return _uof.Semesters.FindById(id);
        }

        public IEnumerable<Semester> Get()
        {
            return _uof.Semesters.Get();
        }
        public void Remove(Guid semesterID)
        {
            _uof.Semesters.Remove(_uof.Semesters.FindById(semesterID));
        }

        #endregion
    }
}
