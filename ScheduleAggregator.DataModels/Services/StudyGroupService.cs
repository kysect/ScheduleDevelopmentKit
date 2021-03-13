using ScheduleAggregator.DataModels.Entities;
using ScheduleAggregator.DataModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

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
            if (_uof.StudyGroups.Get().Any(el => el.Name == name))
                throw new Exception("The StudyGroup already exists");

            var Out = new StudyGroup() { Name = name, StudyCourse = _uof.StudyCourses.FindById(course) };
            _uof.StudyGroups.Create(Out);
            return Out.Id;
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
        
        public void Remove(Guid studyGroupId)
        {
            _uof.StudyGroups.Remove(_uof.StudyGroups.FindById(studyGroupId));
        }

        public void Update(StudyGroup studyGroup)
        {
            _uof.StudyGroups.Update(studyGroup);
        }
        #endregion
    }
}
