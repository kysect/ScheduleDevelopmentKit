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
        private UnitOfWork UOF;
        public ScheduleService(UnitOfWork uof)
        {
            UOF = uof;
        }
        public void Create(string name)
        {
            UOF.Schedules.Create(new Schedule());
        }
        public void Update(Schedule schedule)
        {
            UOF.Schedules.Update(schedule);
        }

        ///

        public Schedule FindByID(int id)
        {
            return UOF.Schedules.FindById(id);
        }

        public IEnumerable<Schedule> Get()
        {
            return UOF.Schedules.Get();
        }
        public void Remove(Schedule schedule)
        {
            UOF.Schedules.Remove(schedule);
        }
    }
}
