﻿using ScheduleAggregator.DataModels.Entities;
using ScheduleAggregator.DataModels.Enums;
using ScheduleAggregator.DataModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleAggregator.DataModels.Services
{
    public class LessonService
    {
        private UnitOfWork _uof;
        public LessonService(UnitOfWork uof)
        {
            _uof = uof;
        }
        public Guid Create(Guid subjectID, LessonType lessonType, Guid groupID)
        {
            var Out = new Lesson() { Subject = _uof.SemesterSubjects.FindById(subjectID), LessonType = lessonType, Group = _uof.StudyGroups.FindById(groupID) };
            _uof.Lessons.Create(Out);
            return Out.Id;
        }
        public void Update(Lesson lesson)
        {
            _uof.Lessons.Update(lesson);
        }

        #region SameOperations
        public Lesson FindByID(Guid id)
        {
            return _uof.Lessons.FindById(id);
        }

        public IEnumerable<Lesson> Get()
        {
            return _uof.Lessons.Get();
        }
        public void Remove(Guid lessonID)
        {
            _uof.Lessons.Remove(_uof.Lessons.FindById(lessonID));
        }
        #endregion
    }
}
