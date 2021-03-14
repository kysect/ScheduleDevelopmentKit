﻿using ScheduleAggregator.DataModels.Entities;
using ScheduleAggregator.DataModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using ScheduleAggregator.DataModels.Interfaces;

namespace ScheduleAggregator.DataModels.Services
{
    public class SubjectService : IService<Subject>
    {
        private readonly UnitOfWork _uof;

        public SubjectService(UnitOfWork uof)
        {
            _uof = uof;
        }

        public Guid Create(string name)
        {
            if (_uof.Subjects.Get().Any(s => s.Name == name))
                throw new Exception("The Subject already exists");

            var result = new Subject() { Name = name };
            _uof.Subjects.Create(result);
            return result.Id;
        }

        public void AddTeacher(Guid subjectId, Guid teacherId)
        {
            var subject = _uof.Subjects.FindById(subjectId);
            if (!subject.Teachers.Exists(t => t.Id == teacherId))
            {
                subject.Teachers.Add(_uof.Teachers.FindById(teacherId));
                _uof.Subjects.Update(subject);
            }
        }

        public void AddSemesterSubject(Guid subjectId, Guid semesterSubjectId)
        {
            var subject = _uof.Subjects.FindById(subjectId);
            var semesterSubject = _uof.SemesterSubjects.FindById(semesterSubjectId);
            if (!subject.SemesterSubjects.Exists(s => s.Id == semesterSubject.Id))
            {
                subject.SemesterSubjects.Add(semesterSubject);
                _uof.Subjects.Update(subject);
            }
        }

        public Subject FindById(Guid id)
        {
            return _uof.Subjects.FindById(id);
        }

        public IEnumerable<Subject> Get()
        {
            return _uof.Subjects.Get();
        }

        public void Remove(Guid subjectId)
        {
            _uof.Subjects.Remove(_uof.Subjects.FindById(subjectId));
        }

        public void Update(Subject subject)
        {
            _uof.Subjects.Update(subject);
        }
    }
}
