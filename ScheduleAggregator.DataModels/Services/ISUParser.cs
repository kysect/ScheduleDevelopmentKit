using ScheduleAggregator.DataModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Kysect.ItmoScheduleSdk;
using Kysect.ItmoScheduleSdk.Models;
using Kysect.ItmoScheduleSdk.Types;
using ScheduleAggregator.DataModels.Enums;

namespace ScheduleAggregator.DataModels.Services
{
    public class IsuParser
    {
        private readonly LessonService lessonService;
        private readonly RoomService roomService;
        private readonly ScheduleService scheduleService;
        private readonly SemesterService semesterService;
        private readonly SemesterSubjectService semesterSubjectService;
        private readonly StudyCourseService studyCourseService;
        private readonly StudyGroupService studyGroupService;
        private readonly SubjectService subjectService;
        private readonly TeacherService teacherService;

        private readonly ItmoApiProvider _provider;
        private readonly List<ScheduleItemModel> _scheduleItems;

        public IsuParser(UnitOfWork uof)
        {
            lessonService = new LessonService(uof);
            roomService = new RoomService(uof);
            scheduleService = new ScheduleService(uof);
            semesterService = new SemesterService(uof);
            semesterSubjectService = new SemesterSubjectService(uof);
            studyCourseService = new StudyCourseService(uof);
            studyGroupService = new StudyGroupService(uof);
            subjectService = new SubjectService(uof);
            teacherService = new TeacherService(uof);

            Init();

            _provider = new ItmoApiProvider();
            _scheduleItems = _provider.ScheduleApi.GetGroupPackSchedule(studyGroupService.Get().Select(el => el.Name).ToList());

            // TODO: https://github.com/kysect/ScheduleAggregator/issues/27
            _scheduleItems.RemoveAll(el => String.IsNullOrEmpty(el.Room) || String.IsNullOrEmpty(el.Place) || String.IsNullOrEmpty(el.Teacher));

        }

        private void Init()
        {
            Guid course = studyCourseService.Create("Fake_Course");
            semesterService.Create("Fake_Semester", course);

            studyGroupService.Create("M32011", course);
            studyGroupService.Create("M32021", course);
            studyGroupService.Create("M32031", course);
            studyGroupService.Create("M32041", course);
            studyGroupService.Create("M32051", course);
            studyGroupService.Create("M32061", course);
            studyGroupService.Create("M32071", course);
            studyGroupService.Create("M32081", course);
            studyGroupService.Create("M32091", course);
            studyGroupService.Create("M32101", course);
            studyGroupService.Create("M32111", course);
            studyGroupService.Create("M32122", course);
        }

        public void ParseFromIsu()
        {
            ParseRooms();
            ParseTeachers();
            ParseSubjects();
            CreateFakeSemesterSubjects();
            ParseLessons();
        }

        private void ParseRooms()
        {
            foreach (var item in _scheduleItems)
            {
                string name = item.Room;
                var campus = ConvertToCampus(item.Place);

                if (roomService.Get().All(s => s.Name != name))
                    roomService.Create(name, campus);
            }
        }

        private void ParseTeachers()
        {
            foreach (var item in _scheduleItems)
            {
                string name = item.Teacher;

                if (teacherService.Get().All(s => s.Name != name))
                    teacherService.Create(name);
            }
        }

        private void ParseSubjects()
        {
            foreach (var item in _scheduleItems)
            {
                string name = item.SubjectTitle;
                
                if (subjectService.Get().All(s => s.Name != name))
                    subjectService.Create(name);
            }
        }

        private void CreateFakeSemesterSubjects()
        {
            Guid fakeSemesterId = semesterService.Get().First(el => el.Name == "Fake_Semester").Id;
            foreach(Guid subjectId in subjectService.Get().Select(el => el.Id))
            {
                semesterSubjectService.Create(subjectId, fakeSemesterId, 0, 0, 0);
            }
        }

        private void ParseLessons()
        {
            foreach (var item in _scheduleItems)
            {
                string name = item.SubjectTitle;
                Guid subjectId = semesterSubjectService.Get().First(el => el.Subject.Name == name).Id;
                LessonType lessonType = ConvertToLessonType(item.Status);
                Guid teacherId = teacherService.Get().First(el => el.Name == item.Teacher).Id;
                Campus campus = ConvertToCampus(item.Place);
                Guid roomId = roomService.Get().First(el => el.Name == item.Room && el.Campus == campus).Id;
                TimeSlot timeSlot = ConvertToTimeSlot(item.StartTime);
                DaySlot daySlot = ConvertToDaySlot(item.DataDay);
                WeekType weekType = ConvertToWeekType(item.DataWeek);
                Guid groupId = studyGroupService.Get().First(el => el.Name == item.Group).Id;

                lessonService.Create(subjectId, lessonType, groupId, teacherId, roomId, timeSlot, daySlot, weekType);
            }
        }

        private LessonType ConvertToLessonType(string lesson)
        {
            lesson = lesson.ToUpper();
            return lesson switch
            {
                "ЛЕК" => LessonType.Lecture,
                "ЛАБ" => LessonType.Laboratory,
                "ПРАК" => LessonType.Practise,
                _ => throw new ArgumentOutOfRangeException(nameof(lesson), lesson, null)
            };
        }

        private Campus ConvertToCampus(string campusName)
        {
            if (string.IsNullOrEmpty(campusName))
                return Campus.Undefined;
            if (campusName.Contains("Биржевая"))
                return Campus.Birjevaya;
            if (campusName.Contains("Ломоносова"))
                return Campus.Lomonosova;
            if (campusName.Contains("Кронверкский"))
                return Campus.Kronverskiy;

            return Campus.Undefined;
        }

        private TimeSlot ConvertToTimeSlot(string startTime)
        {
            return startTime switch
            {
                "08:20" => TimeSlot.Lesson1,
                "10:00" => TimeSlot.Lesson2,
                "11:40" => TimeSlot.Lesson3,
                "13:30" => TimeSlot.Lesson4,
                "15:20" => TimeSlot.Lesson5,
                "17:00" => TimeSlot.Lesson6,
                "18:40" => TimeSlot.Lesson7,
                "20:20" => TimeSlot.Lesson8,
                _ => throw new ArgumentOutOfRangeException(nameof(startTime), startTime, null)
            };
        }

        private DaySlot ConvertToDaySlot(DataDayType? day)
        {
            return day switch
            {
                DataDayType.Monday => DaySlot.Monday,
                DataDayType.Tuesday => DaySlot.Tuesday,
                DataDayType.Wednesday => DaySlot.Wednesday,
                DataDayType.Thursday => DaySlot.Thursday,
                DataDayType.Friday => DaySlot.Friday,
                DataDayType.Saturday => DaySlot.Saturday,
                _ => throw new ArgumentOutOfRangeException(nameof(day), day, null)
            };
        }

        private WeekType ConvertToWeekType(DataWeekType? week)
        {
            return week switch
            {
                DataWeekType.Both => WeekType.Both,
                DataWeekType.Odd => WeekType.Odd,
                DataWeekType.Even => WeekType.Even,
                _ => throw new ArgumentOutOfRangeException(nameof(week), week, null)
            };
        }
    }
}