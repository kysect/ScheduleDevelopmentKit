﻿using ScheduleAggregator.DataModels.Entities;
using ScheduleAggregator.DataModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleAggregator.DataModels.Services
{
    public class StudyCourseService
    {
        private UnitOfWork _uof;
        public StudyCourseService(UnitOfWork uof)
        {
            _uof = uof;
        }
        public Guid Create(string name)
        {
            if (_uof.StudyCourses.Get(_ => _.Name == name) != null)
                throw new Exception("The StudyCource already exists");

            var Out = new StudyCourse() { Name = name };
            _uof.StudyCourses.Create(Out);
            return Out.Id;
        }
        public void AddGroup(Guid courseID, Guid groupID)
        {
            var course = _uof.StudyCourses.FindById(courseID);
            if (!course.Groups.Exists(_ => _.Id == groupID))
            {
                course.Groups.Add(_uof.StudyGroups.FindById(groupID));
                _uof.StudyCourses.Update(course);
            }
        }

        #region SameOperations

        public StudyCourse FindByID(Guid id)
        {
            return _uof.StudyCourses.FindById(id);
        }

        public IEnumerable<StudyCourse> Get()
        {
            return _uof.StudyCourses.Get();
        }
        public void Remove(Guid studyCourseID)
        {
            _uof.StudyCourses.Remove(_uof.StudyCourses.FindById(studyCourseID));
        }
        #endregion
    }
}
