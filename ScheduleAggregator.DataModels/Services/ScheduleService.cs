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
        public void Create()
        {
            _uof.Schedules.Create(new Schedule());
        }
        public void Update(Schedule schedule)
        {
            _uof.Schedules.Update(schedule);
        }

        public void AddLesson(Schedule schedule, Lesson lesson)
        {
            if (!schedule.Lessons.Exists(_ => _.Id == lesson.Id))
            {
                schedule.Lessons.Add(lesson);
                _uof.Schedules.Update(schedule);
            }
        }

        ///

        public Schedule FindByID(int id)
        {
            return _uof.Schedules.FindById(id);
        }

        public IEnumerable<Schedule> Get()
        {
            return _uof.Schedules.Get();
        }
        public void Remove(Schedule schedule)
        {
            _uof.Schedules.Remove(schedule);
        }
    }
}
