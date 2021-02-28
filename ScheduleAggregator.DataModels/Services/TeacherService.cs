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
        private UnitOfWork UOF;
        public TeacherService(UnitOfWork uof)
        {
            UOF = uof;
        }
        public void Create(string name)
        {
            if (UOF.Teachers.Get(_ => _.Name == name) != null)
                throw new Exception("The Teacher already exists");

            UOF.Teachers.Create(new Teacher() { Name = name });
        }
        public void Update(Teacher teacher)
        {
            UOF.Teachers.Update(teacher);
        }

        ///

        public Teacher FindByID(int id)
        {
            return UOF.Teachers.FindById(id);
        }

        public IEnumerable<Teacher> Get()
        {
            return UOF.Teachers.Get();
        }
        public void Remove(Teacher item)
        {
            UOF.Teachers.Remove(item);
        }
    }
}
