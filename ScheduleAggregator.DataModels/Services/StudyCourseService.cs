﻿using ScheduleAggregator.DataModels.Entities;
using ScheduleAggregator.DataModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using ScheduleAggregator.DataModels.Interfaces;

namespace ScheduleAggregator.DataModels.Services
{
    public class StudyCourseService : IService<StudyCourse>
    {
        private readonly UnitOfWork _uof;

        public StudyCourseService(UnitOfWork uof)
        {
            _uof = uof;
        }

        public Guid Create(string name)
        {
            if (_uof.StudyCourses.Get().Any(c => c.Name == name))
                throw new Exception("The StudyCourse already exists");

            var result = new StudyCourse() { Name = name };
            _uof.StudyCourses.Create(result);
            return result.Id;
        }

        public void AddGroup(Guid courseId, Guid groupId)
        {
            var course = _uof.StudyCourses.FindById(courseId);
            if (!course.Groups.Exists(g => g.Id == groupId))
            {
                course.Groups.Add(_uof.StudyGroups.FindById(groupId));
                _uof.StudyCourses.Update(course);
            }
        }

        public void AddSemester(Guid courseId, Guid semesterId)
        {
            var course = _uof.StudyCourses.FindById(courseId);
            if (!course.Semesters.Exists(s => s.Id == semesterId))
            {
                course.Semesters.Add(_uof.Semesters.FindById(semesterId));
                _uof.StudyCourses.Update(course);
            }
        }

        public StudyCourse FindById(Guid id)
        {
            return _uof.StudyCourses.FindById(id);
        }

        public IEnumerable<StudyCourse> Get()
        {
            return _uof.StudyCourses.Get();
        }

        public void Remove(Guid studyCourseId)
        {
            _uof.StudyCourses.Remove(_uof.StudyCourses.FindById(studyCourseId));
        }
        
        public void Update(StudyCourse studyCourse)
        {
            _uof.StudyCourses.Update(studyCourse);
        }
    }
}
