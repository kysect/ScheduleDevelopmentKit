using System.Collections.Generic;
using ScheduleDevelopmentKit.DataModels.Entities;
using ScheduleDevelopmentKit.DataModels.Repositories;
using ScheduleDevelopmentKit.DataModels.Services;

namespace ScheduleDevelopmentKit.Ui.Tools
{
    public class ExecutionContext
    {
        public static ExecutionContext Instance = new ExecutionContext();

        private readonly UnitOfWork _uof;
        private readonly ScheduleContext _db;

        public LessonService LessonService { get; private set; }
        public RoomService RoomService { get; private set; }
        public ScheduleService ScheduleService { get; private set; }
        public SemesterService SemesterService { get; private set; }
        public SemesterSubjectService SemesterSubjectService { get; private set; }
        public StudyCourseService StudyCourseService { get; private set; }
        public StudyGroupService StudyGroupService { get; private set; }
        public SubjectService SubjectService { get; private set; }
        public TeacherService TeacherService { get; private set; }

        public IsuParser IsuParser { get; private set; }
        public ExecutionContext()
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