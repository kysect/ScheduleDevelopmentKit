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
        private UnitOfWork UOF;
        public StudyGroupService(UnitOfWork uof)
        {
            UOF = uof;
        }
        public void Create(string name)
        {
            if (UOF..Get(_ => _.Name == name) != null)
                throw new Exception("The StudyGroup already exists");

            UOF.StudyGroups.Create(new StudyGroup() { Name = name });
        }
        public void Update(StudyGroup studyGroup)
        {
            UOF.StudyGroups.Update(studyGroup);
        }

        ///

        public StudyGroup FindByID(int id)
        {
            return UOF.StudyGroups.FindById(id);
        }

        public IEnumerable<StudyGroup> Get()
        {
            return UOF.StudyGroups.Get();
        }
        public void Remove(StudyGroup studyGroup)
        {
            UOF.StudyGroups.Remove(studyGroup);
        }
    }
}
