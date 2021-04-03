using ScheduleDevelopmentKit.DataModels.Entities;
using ScheduleDevelopmentKit.DataModels.Enums;
using ScheduleDevelopmentKit.DataModels.Repositories;
using System;
using System.Collections.Generic;
using ScheduleDevelopmentKit.DataModels.Interfaces;

namespace ScheduleDevelopmentKit.DataModels.Services
{
    public class LessonService : IService<Lesson>
    {
        private readonly UnitOfWork _uof;

        public LessonService(UnitOfWork uof)
        {
            _uof = uof;
        }

        public Guid Create(Guid subjectId, LessonType lessonType, Guid groupId, Guid teacherId, Guid roomId, TimeSlot timeSlot, DaySlot daySlot, WeekType weekType)
        {
            var result = new Lesson() {
                Subject = _uof.SemesterSubjects.FindById(subjectId),
                LessonType = lessonType,
                Group = _uof.StudyGroups.FindById(groupId),
                Teacher = _uof.Teachers.FindById(teacherId),
                Room = _uof.Rooms.FindById(roomId),
                TimeSlot = timeSlot,
                DaySlot = daySlot,
                WeekType = weekType
            };
            _uof.Lessons.Create(result);
            return result.Id;
        }

        public void AssignLessonToTeacher(Guid teacherId, Guid lessonId)
        {
            var teacher = _uof.Teachers.FindById(teacherId);
            var lesson = _uof.Lessons.FindById(lessonId);
            if (!teacher.Lessons.Exists(l => l.Id == lesson.Id))
            {
                teacher.Lessons.Add(lesson);
                _uof.Teachers.Update(teacher);
            }
        }

        public Lesson FindById(Guid id)
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
    }
}
