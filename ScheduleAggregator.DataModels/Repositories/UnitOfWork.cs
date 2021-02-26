using Microsoft.EntityFrameworkCore;
using ScheduleAggregator.DataModels;
using ScheduleAggregator.DataModels.Entities;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleAggregator.DataModels.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private ScheduleContext _db;
        public IGenericRepository<LabourIntensity> LabourIntensities { get;private set; }
        public IGenericRepository<Lesson> Lessons { get; private set; }
        public IGenericRepository<Room> Rooms{ get; private set; }
        public IGenericRepository<Schedule> Schedules{ get; private set; }
        public IGenericRepository<Semester> Semesters{ get; private set; }
        public IGenericRepository<SemesterSubject> SemesterSubjects{ get; private set; }
        public IGenericRepository<StudyCourse> StudyCourses{ get; private set; }
        public IGenericRepository<Subject> Subjects{ get; private set; }
        public IGenericRepository<Teacher> Teachers{ get; private set; }


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
        }

        public void Save()
        {
            _db.SaveChanges();
        }
        public async void SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
