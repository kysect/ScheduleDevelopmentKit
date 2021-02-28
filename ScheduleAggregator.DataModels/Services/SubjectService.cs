using ScheduleAggregator.DataModels.Entities;
using ScheduleAggregator.DataModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleAggregator.DataModels.Services
{
    public class SubjectService
    {
        private UnitOfWork UOF;
        public SubjectService(UnitOfWork uof)
        {
            UOF = uof;
        }
        public void Create(string name)
        {
            if (UOF.Subjects.Get(_ => _.Name == name) != null)
                throw new Exception("The Subject already exists");

            UOF.Subjects.Create(new Subject() { Name = name });
        }
        public void Update(Subject subject)
        {
            UOF.Subjects.Update(subject);
        }

        ///

        public Subject FindByID(int id)
        {
            return UOF.Subjects.FindById(id);
        }

        public IEnumerable<Subject> Get()
        {
            return UOF.Subjects.Get();
        }
        public void Remove(Subject subject)
        {
            UOF.Subjects.Remove(subject);
        }
    }
}
