using ScheduleAggregator.DataModels.Entities;
using ScheduleAggregator.DataModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleAggregator.DataModels.Services
{
    public class StudyGroupService
    {
        private UnitOfWork _uof;
        public StudyGroupService(UnitOfWork uof)
        {
            _uof = uof;
        }
        public void Create(string name,StudyCourse course)
        {
            if (_uof.StudyGroups.Get(_ => _.Name == name) != null)
                throw new Exception("The StudyGroup already exists");

            _uof.StudyGroups.Create(new StudyGroup() { Name = name, StudyCourse = course });
        }
        public void Update(StudyGroup studyGroup)
        {
            _uof.StudyGroups.Update(studyGroup);
        }

        ///

        public StudyGroup FindByID(int id)
        {
            return _uof.StudyGroups.FindById(id);
        }

        public IEnumerable<StudyGroup> Get()
        {
            return _uof.StudyGroups.Get();
        }
        public void Remove(StudyGroup studyGroup)
        {
            _uof.StudyGroups.Remove(studyGroup);
        }
    }
}
