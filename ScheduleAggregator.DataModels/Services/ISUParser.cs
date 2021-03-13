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
        private LessonService lessonService;
        private RoomService roomService;
        private ScheduleService scheduleServide;
        private SemesterService semesterService;
        private SemesterSubjectService semesterSubjectService;
        private StudyCourseService studyCourseService;
        private StudyGroupService studyGroupService;
        private SubjectService subjectService;
        private TeacherService teacherService;

        private readonly ItmoApiProvider _provider;
        private readonly List<ScheduleItemModel> _scheduleItems;

        public IsuParser(UnitOfWork uof)
        {
            lessonService = new LessonService(uof);
            roomService = new RoomService(uof);
            scheduleServide = new ScheduleService(uof);
            semesterService = new SemesterService(uof);
            semesterSubjectService = new SemesterSubjectService(uof);
            studyCourseService = new StudyCourseService(uof);
            studyGroupService = new StudyGroupService(uof);
            subjectService = new SubjectService(uof);
            teacherService = new TeacherService(uof);

            Init();

            _provider = new ItmoApiProvider();
            _scheduleItems = _provider.ScheduleApi.GetGroupPackSchedule(studyGroupService.Get().Select(el => el.Name).ToList());


            // На данный момент я не знаю как обращаться с дистанционными предметами
            // так как у них нет корпуса и аудитории при создании таких предметов возникают сложности
            // пока просто исключу из расписания

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
            studyGroupService.Create("M32121", course);
        }

        public void ParseFromISU()
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

                if (!roomService.Get(s => s.Name == name).Any())
                    roomService.Create(name, campus);
            }
        }

        private void ParseTeachers()
        {
            foreach (var item in _scheduleItems)
            {
                string name = item.Teacher;

                if (!teacherService.Get(s => s.Name == name).Any())
                    teacherService.Create(name);
            }
        }

        private void ParseSubjects()
        {
            foreach (var item in _scheduleItems)
            {
                string name = item.SubjectTitle;
                
                if (!subjectService.Get(s => s.Name == name).Any())
                    subjectService.Create(name);
            }
        }

        private void CreateFakeSemesterSubjects()
        {
            Guid fakeSemesterID = semesterService.Get(el => el.Name == "Fake_Semester").First().Id;
            foreach(var subjectID in subjectService.Get().Select(el => el.Id))
            {
                semesterSubjectService.Create(subjectID, fakeSemesterID, 0, 0, 0);
            }
        }

        private void ParseLessons()
        {
            foreach (var item in _scheduleItems)
            {
                string name = item.SubjectTitle;
                Guid subjectID = semesterSubjectService.Get(el => el.Subject.Name == name).First().Id;
                var lessonType = ConvertToLessonType(item.Status);
                Guid teacherID = teacherService.Get(el => el.Name == item.Teacher).First().Id;
                Campus campus = ConvertToCampus(item.Place);
                Guid roomID = roomService.Get(el => el.Name == item.Room && el.Campus == campus).First().Id;
                var timeSlot = ConvertToTimeSlot(item.StartTime);
                var daySlot = ConvertToDaySlot(item.DataDay);
                WeekType weekType = ConvertToWeekType(item.DataWeek);
                Guid groupID = studyGroupService.Get(el => el.Name == item.Group).First().Id;

                lessonService.Create(subjectID, lessonType, groupID, teacherID, roomID, timeSlot, daySlot, weekType);
            }
        }

        private LessonType ConvertToLessonType(string lesson)
        {
            lesson = lesson.ToUpper();
            return lesson switch
            {
                "ЛЕК" => LessonType.Lecture,
                "ЛАБ" => LessonType.Laboratory,
                "ПРАК" => LessonType.Practise
            };
        }

        

        private Campus ConvertToCampus(string campusName)
        {
            if (String.IsNullOrEmpty(campusName))
                return Campus.Undefined;
            if (campusName.Contains("Биржевая линия"))
                return Campus.Birjevaya;
            if (campusName.Contains("Ломоносова"))
                return Campus.Lomonosova;
            if (campusName.Contains("Кронверкский"))
                return Campus.Kronverskiy;

            else return Campus.Undefined;
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
                _ => throw new ArgumentOutOfRangeException("Invalid Time")
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
            };
        }

        private WeekType ConvertToWeekType(DataWeekType? week)
        {
            return week switch
            {
                DataWeekType.Both => WeekType.Both,
                DataWeekType.Odd => WeekType.Odd,
                DataWeekType.Even => WeekType.Even
            };
        }
    }
}