using ScheduleAggregator.DataModels.Repositories;
using ScheduleAggregator.DataModels.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleAggregator.DataModels
{
    public static class ExecutionContext
    {
        private static readonly UnitOfWork _uof;
        private static readonly ScheduleContext _db; 

        public static LessonService LessonService { get; private set; }
        public static RoomService RoomService{ get; private set; }
        public static ScheduleService ScheduleService{ get; private set; }
        public static SemesterService SemesterService { get; private set; }
        public static SemesterSubjectService SemesterSubjectService { get; private set; }
        public static StudyCourseService StudyCourseService { get; private set; }
        public static StudyGroupService StudyGroupService { get; private set; }
        public static SubjectService SubjectService { get; private set; }
        public static TeacherService TeacherService { get; private set; }

        public static IsuParser IsuParser { get; private set; }
        static ExecutionContext()
        {
            _db = new ScheduleContext();
            _uof = new UnitOfWork(_db);

            LessonService = new LessonService(_uof);
            RoomService = new RoomService(_uof);
            ScheduleService = new ScheduleService(_uof);
            SemesterService = new SemesterService(_uof);
            StudyGroupService = new StudyGroupService(_uof);
            StudyCourseService = new StudyCourseService(_uof);
            SubjectService = new SubjectService(_uof);
            TeacherService = new TeacherService(_uof);
            SemesterSubjectService = new SemesterSubjectService(_uof);
            IsuParser = new IsuParser(_uof);
        }

        
    }
}
