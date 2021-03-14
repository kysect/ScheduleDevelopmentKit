﻿using ScheduleAggregator.DataModels.Entities;
using ScheduleAggregator.DataModels.Repositories;
using System;
using System.Collections.Generic;

namespace ScheduleAggregator.DataModels.Services
{
    public class ScheduleService
    {
        private UnitOfWork _uof;
        public ScheduleService(UnitOfWork uof)
        {
            _uof = uof;
        }
        public Guid Create()
        {
            var Out = new Schedule();
            _uof.Schedules.Create(Out);
            return Out.Id;
        }
        public void AddLesson(Guid scheduleID, Guid lessonID)
        {
            var schedule = _uof.Schedules.FindById(scheduleID);
            if (!schedule.Lessons.Exists(el => el.Id == lessonID))
            {
                schedule.Lessons.Add(_uof.Lessons.FindById(lessonID));
                _uof.Schedules.Update(schedule);
            }
        }

        #region SameOperations

        public Schedule FindByID(Guid id)
        {
            return _uof.Schedules.FindById(id);
        }

        public IEnumerable<Schedule> Get()
        {
            return _uof.Schedules.Get();
        }
        
        public void Remove(Guid scheduleId)
        {
            _uof.Schedules.Remove(_uof.Schedules.FindById(scheduleId));
        }

        public void Update(Schedule schedule)
        {
            _uof.Schedules.Update(schedule);
        }
        #endregion
    }
}
