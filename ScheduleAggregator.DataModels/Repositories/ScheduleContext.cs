using Microsoft.EntityFrameworkCore;
using ScheduleAggregator.DataModels;
using System;
using System.Collections.Generic;
using System.Text;


namespace TryEF
{
    public class ScheduleContext : DbContext
    {

        public ScheduleContext(DbContextOptions<ScheduleContext> options)
        : base(options)
        { }

        public DbSet<LabourIntensity> LabourIntensities{ get; set; }
        public DbSet<Lesson> Lessons{ get; set; }
        public DbSet<Room> Rooms{ get; set; }
        public DbSet<Schedule> Schedules{ get; set; }
        public DbSet<Semester> Semesters{ get; set; }
        public DbSet<SemesterSubject> SemesterSubjects{ get; set; }
        public DbSet<StudyCourse> StudyCourses{ get; set; }
        public DbSet<StudyGroup> StudyGroups{ get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
    }
}
