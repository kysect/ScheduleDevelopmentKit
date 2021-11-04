using System;
using System.Collections.Generic;
using ScheduleDevelopmentKit.Domain.Entities;
using ScheduleDevelopmentKit.LegacyCore;
using ScheduleDevelopmentKit.LegacyCore.Interfaces;

namespace ScheduleDevelopmentKit.LegacyCore.Services
{
    public class ScheduleService : IService<Schedule>
    {
        private readonly UnitOfWork _uof;

        public ScheduleService(UnitOfWork uof)
        {
            _uof = uof;
        }

        public Guid Create()
        {
            var result = new Schedule();
            _uof.Schedules.Create(result);
            return result.Id;
        }

        public void AddLesson(Guid scheduleId, Guid lessonId)
        {
            var schedule = _uof.Schedules.FindById(scheduleId);
            if (!schedule.Lessons.Exists(l => l.Id == lessonId))
            {
                schedule.Lessons.Add(_uof.Lessons.FindById(lessonId));
                _uof.Schedules.Update(schedule);
            }
        }

        public Schedule FindById(Guid id)
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
    }
}
