using ScheduleAggregator.DataModels.Entities;
using ScheduleAggregator.DataModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleAggregator.DataModels.Services
{
    public class StudyCourseService
    {
        private UnitOfWork UOF;
        public StudyCourseService(UnitOfWork uof)
        {
            UOF = uof;
        }
        public void Create(string name)
        {
            if (UOF.StudyCourses.Get(_ => _.Name == name) != null)
                throw new Exception("The StudyCource already exists");

            UOF.StudyCourses.Create(new StudyCourse() { Name = name });
        }
        public void Update(StudyCourse studyCourse)
        {
            UOF.StudyCourses.Update(studyCourse);
        }

        ///

        public StudyCourse FindByID(int id)
        {
            return UOF.StudyCourses.FindById(id);
        }

        public IEnumerable<StudyCourse> Get()
        {
            return UOF.StudyCourses.Get();
        }
        public void Remove(StudyCourse studyCourse)
        {
            UOF.StudyCourses.Remove(studyCourse);
        }
    }
}
