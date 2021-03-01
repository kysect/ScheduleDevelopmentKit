using ScheduleAggregator.DataModels.Entities;
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
        public Lesson Create(SemesterSubject subject, LessonType lessonType, StudyGroup group)
        {
            var Out = new Lesson() { Subject = subject, LessonType = lessonType, Group = group };
            _uof.Lessons.Create(Out);
            return Out;
        }
        public void Update(Lesson lesson)
        {
            _uof.Lessons.Update(lesson);
        }

        #region SameOperations
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
        #endregion
    }
}
