using ScheduleAggregator.DataModels.Entities;
using ScheduleAggregator.DataModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleAggregator.DataModels.Services
{
    public class SemesterSubjectService
    {
        private UnitOfWork UOF;
        public SemesterSubjectService(UnitOfWork uof)
        {
            UOF = uof;
        }
        public void Create(Subject subject)
        {
            if (UOF.SemesterSubjects.Get(_ => _.Subject == subject) != null)
                throw new Exception("The SemesterSubject already exists");

            UOF.SemesterSubjects.Create(new SemesterSubject() { Subject = subject});
        }
        public void Update(SemesterSubject semesterSubject)
        {
            UOF.SemesterSubjects.Update(semesterSubject);
        }

        ///

        public SemesterSubject FindByID(int id)
        {
            return UOF.SemesterSubjects.FindById(id);
        }

        public IEnumerable<SemesterSubject> Get()
        {
            return UOF.SemesterSubjects.Get();
        }
        public void Remove(SemesterSubject studyCourse)
        {
            UOF.SemesterSubjects.Remove(studyCourse);
        }
    }
}
