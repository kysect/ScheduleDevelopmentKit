using System;
using Microsoft.EntityFrameworkCore;
using ScheduleDevelopmentKit.DataModels.Repositories;
using ScheduleDevelopmentKit.DataModels.Services;

namespace ScheduleDevelopmentKit.Desktop.Tools
{
    public class ExecutionContext
    {
        public static readonly ExecutionContext Instance = Create();

        private static ExecutionContext Create()
        {
            var scheduleContext =
                new ScheduleContext(
                    new DbContextOptionsBuilder<ScheduleContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString())
                        .Options);

            return new ExecutionContext(scheduleContext);
        }

        private readonly UnitOfWork _unitOfWork;

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

        public ExecutionContext(ScheduleContext context)
        {
            _unitOfWork = new UnitOfWork(context);

            LessonService = new LessonService(_unitOfWork);
            RoomService = new RoomService(_unitOfWork);
            ScheduleService = new ScheduleService(_unitOfWork);
            SemesterService = new SemesterService(_unitOfWork);
            StudyGroupService = new StudyGroupService(_unitOfWork);
            StudyCourseService = new StudyCourseService(_unitOfWork);
            SubjectService = new SubjectService(_unitOfWork);
            TeacherService = new TeacherService(_unitOfWork);
            SemesterSubjectService = new SemesterSubjectService(_unitOfWork);
            IsuParser = new IsuParser(_unitOfWork);
        }
    }
}