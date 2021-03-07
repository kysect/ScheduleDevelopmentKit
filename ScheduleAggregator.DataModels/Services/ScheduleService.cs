using ScheduleAggregator.DataModels.Entities;
using ScheduleAggregator.DataModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (!schedule.Lessons.Exists(_ => _.Id == lessonID))
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

        public IEnumerable<Schedule> Get(Func<Schedule, bool> predicate)
        {
            return _uof.Schedules.Get(predicate);
        }

        public void Remove(Guid scheduleID)
        {
            _uof.Schedules.Remove(_uof.Schedules.FindById(scheduleID));
        }

        #endregion
    }
}
