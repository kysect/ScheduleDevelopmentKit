using ScheduleAggregator.DataModels.Entities;
using ScheduleAggregator.DataModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleAggregator.DataModels.Services
{
    public class SemesterSubjectService
    {
        private UnitOfWork _uof;
        public SemesterSubjectService(UnitOfWork uof)
        {
            _uof = uof;
        }
        public Guid Create(Guid subjectID, Guid semesterID, uint lecture, uint practise, uint laboratory)
        {
            if (_uof.SemesterSubjects.Get(_ => _.Subject.Id == subjectID) != null)
                throw new Exception("The SemesterSubject already exists");

            var labourIntensity = new LabourIntensity() { Laboratory = laboratory, Lecture = lecture, Practise = practise};
            var Out = new SemesterSubject() { 
                Subject = _uof.Subjects.FindById(subjectID),
                LabourIntensity = labourIntensity,
                Semester = _uof.Semesters.FindById(semesterID)
            };
            _uof.SemesterSubjects.Create(Out);
            return Out.Id;
        }
        #region SameOperations

        public SemesterSubject FindByID(Guid id)
        {
            return _uof.SemesterSubjects.FindById(id);
        }

        public IEnumerable<SemesterSubject> Get()
        {
            return _uof.SemesterSubjects.Get();
        }
        public void Remove(Guid studyCourseID)
        {
            _uof.SemesterSubjects.Remove(_uof.SemesterSubjects.FindById(studyCourseID));
        }
        #endregion 
    }
}
