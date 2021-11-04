using System;
using System.Collections.Generic;
using System.Linq;
using ScheduleDevelopmentKit.Domain.Entities;
using ScheduleDevelopmentKit.Domain.Interfaces;
using ScheduleDevelopmentKit.Domain.Repositories;

namespace ScheduleDevelopmentKit.Domain.Services
{
    public class StudyStreamService : IService<StudyStream>
    {
        private readonly UnitOfWork _uof;

        public StudyStreamService(UnitOfWork uof)
        {
            _uof = uof;
        }

        public Guid Create(string name)
        {
            if (_uof.StudyStreams.Get().Any(x => x.Name == name))
                throw new ArgumentException($"Study stream with name {name} already exists");

            StudyStream studyStream = new StudyStream() { Name = name };
            _uof.StudyStreams.Create(studyStream);
            return studyStream.Id;
        }

        public StudyStream FindById(Guid id)
        {
            return _uof.StudyStreams.FindById(id);
        }

        public IEnumerable<StudyStream> Get()
        {
            return _uof.StudyStreams.Get();
        }

        public void Remove(Guid id)
        {
            _uof.StudyStreams.Remove(FindById(id));
        }

        public void Update(StudyStream entity)
        {
            _uof.StudyStreams.Update(entity);
        }
    }
}