using ScheduleDevelopmentKit.DataModels.Entities;
using System;

namespace ScheduleDevelopmentKit.DataModels.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private readonly ScheduleContext _db;
        private bool _disposed = false;

        public IGenericRepository<LabourIntensity> LabourIntensities { get; }
        public IGenericRepository<Lesson> Lessons { get; }
        public IGenericRepository<Room> Rooms{ get; }
        public IGenericRepository<Schedule> Schedules{ get; }
        public IGenericRepository<Semester> Semesters{ get; }
        public IGenericRepository<SemesterSubject> SemesterSubjects{ get; }
        public IGenericRepository<StudyCourse> StudyCourses{ get; }
        public IGenericRepository<Subject> Subjects{ get; }
        public IGenericRepository<Teacher> Teachers{ get; }
        public IGenericRepository<StudyGroup> StudyGroups { get; }
        public IGenericRepository<StudyStream> StudyStreams { get; }

        public UnitOfWork(ScheduleContext db)
        {
            _db = db;
            LabourIntensities = new GenericRepository<LabourIntensity>(db, db.LabourIntensities);
            Lessons= new GenericRepository<Lesson>(db, db.Lessons);
            Rooms = new GenericRepository<Room>(db, db.Rooms);
            Schedules = new GenericRepository<Schedule>(db, db.Schedules);
            Semesters = new GenericRepository<Semester>(db, db.Semesters);
            SemesterSubjects= new GenericRepository<SemesterSubject>(db, db.SemesterSubjects);
            StudyCourses= new GenericRepository<StudyCourse>(db, db.StudyCourses);
            Subjects = new GenericRepository<Subject>(db, db.Subjects);
            Teachers = new GenericRepository<Teacher>(db, db.Teachers);
            StudyGroups = new GenericRepository<StudyGroup>(db, db.StudyGroups);
            StudyStreams = new GenericRepository<StudyStream>(db, db.StudyStreams);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public async void SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this._disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
