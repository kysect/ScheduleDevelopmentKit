using ScheduleAggregator.DataModels.Entities;
using ScheduleAggregator.DataModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleAggregator.DataModels.Services
{
    public class TeacherService
    {
        private UnitOfWork _uof;
        public TeacherService(UnitOfWork uof)
        {
            _uof = uof;
        }
        public void Create(string name)
        {
            if (_uof.Teachers.Get(_ => _.Name == name) != null)
                throw new Exception("The Teacher already exists");

            _uof.Teachers.Create(new Teacher() { Name = name });
        }
        public void Update(Teacher teacher)
        {
            _uof.Teachers.Update(teacher);
        }

        ///

        public Teacher FindByID(int id)
        {
            return _uof.Teachers.FindById(id);
        }

        public IEnumerable<Teacher> Get()
        {
            return _uof.Teachers.Get();
        }
        public void Remove(Teacher item)
        {
            _uof.Teachers.Remove(item);
        }
    }
}
