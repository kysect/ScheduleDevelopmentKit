using ScheduleDevelopmentKit.DataModels.Entities;
using ScheduleDevelopmentKit.DataModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using ScheduleDevelopmentKit.DataModels.Interfaces;

namespace ScheduleDevelopmentKit.DataModels.Services
{
    public class StudyGroupService : IService<StudyGroup>
    {
        private readonly UnitOfWork _uof;

        public StudyGroupService(UnitOfWork uof)
        {
            _uof = uof;
        }

        public Guid Create(string name, Guid course)
        { 
            if (_uof.StudyGroups.Get().Any(g => g.Name == name))
                throw new Exception("The StudyGroup already exists");

            var result = new StudyGroup() { Name = name, StudyCourse = _uof.StudyCourses.FindById(course) };
            _uof.StudyGroups.Create(result);
            return result.Id;
        }

        public StudyGroup FindById(Guid id)
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
    }
}
