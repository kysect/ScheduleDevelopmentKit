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
        public Guid Create(string name, Guid course)
        { 
            if (_uof.StudyGroups.Get(_ => _.Name == name) != null)
                throw new Exception("The StudyGroup already exists");

            var Out = new StudyGroup() { Name = name, StudyCourse = _uof.StudyCourses.FindById(course) };
            _uof.StudyGroups.Create(Out);
            return Out.Id;
        }
        public void Update(StudyGroup studyGroup)
        {
            _uof.StudyGroups.Update(studyGroup);
        }

        #region SameOperations

        public StudyGroup FindByID(Guid id)
        {
            return _uof.StudyGroups.FindById(id);
        }

        public IEnumerable<StudyGroup> Get()
        {
            return _uof.StudyGroups.Get();
        }
        public void Remove(Guid studyGroupID)
        {
            _uof.StudyGroups.Remove(_uof.StudyGroups.FindById(studyGroupID));
        }

        #endregion
    }
}
