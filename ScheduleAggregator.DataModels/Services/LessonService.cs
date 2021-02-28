using ScheduleAggregator.DataModels.Entities;
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
        private UnitOfWork UOF;
        public LessonService(UnitOfWork uof)
        {
            UOF = uof;
        }
        public void Create(string name)
        {
            UOF.Lessons.Create(new Lesson());
        }
        public void Update(Lesson lesson)
        {
            UOF.Lessons.Update(lesson);
        }

        ///

        public Lesson FindByID(int id)
        {
            return UOF.Lessons.FindById(id);
        }

        public IEnumerable<Lesson> Get()
        {
            return UOF.Lessons.Get();
        }
        public void Remove(Lesson lesson)
        {
            UOF.Lessons.Remove(lesson);
        }
    }
}
