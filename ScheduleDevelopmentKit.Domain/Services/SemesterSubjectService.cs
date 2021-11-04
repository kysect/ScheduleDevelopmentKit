using System;
using System.Collections.Generic;
using System.Linq;
using ScheduleDevelopmentKit.Domain.Entities;
using ScheduleDevelopmentKit.Domain.Repositories;
using ScheduleDevelopmentKit.Domain.Interfaces;

namespace ScheduleDevelopmentKit.Domain.Services
{
    public class SemesterSubjectService : IService<SemesterSubject>
    {
        private readonly UnitOfWork _uof;

        public SemesterSubjectService(UnitOfWork uof)
        {
            _uof = uof;
        }

        public Guid Create(Guid subjectId, Guid semesterId, uint lecture, uint practice, uint laboratory)
        {
            if (_uof.SemesterSubjects.Get().Any(s => s.Subject.Id == subjectId))
                throw new Exception("The SemesterSubject already exists");

            var labourIntensity = new LabourIntensity() { Laboratory = laboratory, Lecture = lecture, Practise = practice };
            var result = new SemesterSubject()
            {
                Subject = _uof.Subjects.FindById(subjectId),
                LabourIntensity = labourIntensity,
                Semester = _uof.Semesters.FindById(semesterId)
            };
            _uof.SemesterSubjects.Create(result);
            return result.Id;
        }

        public SemesterSubject FindById(Guid id)
        {
            return _uof.SemesterSubjects.FindById(id);
        }

        public IEnumerable<SemesterSubject> Get()
        {
            return _uof.SemesterSubjects.Get();
        }

        public void Remove(Guid studyCourseId)
        {
            _uof.SemesterSubjects.Remove(_uof.SemesterSubjects.FindById(studyCourseId));
        }

        public void Update(SemesterSubject semesterSubject)
        {
            _uof.SemesterSubjects.Update(semesterSubject);
        }
    }
}
