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
        private UnitOfWork _uof;
        public SemesterService(UnitOfWork uof)
        {
            _uof = uof;
        }
        public void Create(string name, StudyCourse course)
        {
            if (_uof.Semesters.Get(_ => _.Name == name) != null)
                throw new Exception("The Semester already exists");

            _uof.Semesters.Create(new Semester() { Name = name, StudyCourse = course});
        }
        public void Update(Semester semester)
        {
            _uof.Semesters.Update(semester);
        }

        ///

        public Semester FindByID(int id)
        {
            return _uof.Semesters.FindById(id);
        }

        public IEnumerable<Semester> Get()
        {
            return _uof.Semesters.Get();
        }
        public void Remove(Semester semester)
        {
            _uof.Semesters.Remove(semester);
        }
    }
}
