using ScheduleAggregator.DataModels.Entities;
using ScheduleAggregator.DataModels.Enums;
using ScheduleAggregator.DataModels.Repositories;
using System;
using System.Collections.Generic;

namespace ScheduleAggregator.DataModels.Services
{
    public class LessonService
    {
        private readonly UnitOfWork _uof;

        public LessonService(UnitOfWork uof)
        {
            _uof = uof;
        }
        public Guid Create(Guid subjectID, LessonType lessonType, Guid groupID, Guid teacherID, Guid roomID, TimeSlot timeSlot, DaySlot daySlot, WeekType weekType)
        {
            var Out = new Lesson() {
                Subject = _uof.SemesterSubjects.FindById(subjectID),
                LessonType = lessonType,
                Group = _uof.StudyGroups.FindById(groupID),
                Teacher = _uof.Teachers.FindById(teacherID),
                Room = _uof.Rooms.FindById(roomID),
                TimeSlot = timeSlot,
                DaySlot = daySlot,
                WeekType = weekType
            };
            _uof.Lessons.Create(Out);
            return Out.Id;
        }

        public void AssignLessonToTeacher(Guid teacherID, Guid lessonID)
        {
            var teacher = _uof.Teachers.FindById(teacherID);
            var lesson = _uof.Lessons.FindById(lessonID);
            if (!teacher.Lessons.Exists(el => el.Id == lesson.Id))
            {
                teacher.Lessons.Add(lesson);
                _uof.Teachers.Update(teacher);
            }
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
        
        public void Remove(Guid lessonId)
        {
            _uof.Lessons.Remove(_uof.Lessons.FindById(lessonId));
        }

        public void Update(Lesson lesson)
        {
            _uof.Lessons.Update(lesson);
        }
        #endregion
    }
}
