using ScheduleAggregator.DataModels.Entities;
using ScheduleAggregator.DataModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleAggregator.DataModels.Services
{
    public class SemesterService
    {
        private UnitOfWork UOF;
        public SemesterService(UnitOfWork uof)
        {
            UOF = uof;
        }
        public void Create(string name)
        {
            if (UOF.Semesters.Get(_ => _.Name == name) != null)
                throw new Exception("The Semester already exists");

            UOF.Semesters.Create(new Semester() { Name = name});
        }
        public void Update(Semester semester)
        {
            UOF.Semesters.Update(semester);
        }

        ///

        public Semester FindByID(int id)
        {
            return UOF.Semesters.FindById(id);
        }

        public IEnumerable<Semester> Get()
        {
            return UOF.Semesters.Get();
        }
        public void Remove(Semester semester)
        {
            UOF.Semesters.Remove(semester);
        }
    }
}
