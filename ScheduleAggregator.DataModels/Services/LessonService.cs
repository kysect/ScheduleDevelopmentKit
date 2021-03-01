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
        private UnitOfWork _uof;
        public LessonService(UnitOfWork uof)
        {
            _uof = uof;
        }
        public void Create(string name)
        {
            _uof.Lessons.Create(new Lesson());
        }
        public void Update(Lesson lesson)
        {
            _uof.Lessons.Update(lesson);
        }

        ///

        public Lesson FindByID(int id)
        {
            return _uof.Lessons.FindById(id);
        }

        public IEnumerable<Lesson> Get()
        {
            return _uof.Lessons.Get();
        }
        public void Remove(Lesson lesson)
        {
            _uof.Lessons.Remove(lesson);
        }
    }
}
