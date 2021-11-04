using System;
using System.Collections.Generic;
using System.Linq;
using ScheduleDevelopmentKit.Domain.Enums;
using ScheduleDevelopmentKit.Domain.Entities;
using ScheduleDevelopmentKit.LegacyCore.Interfaces;

namespace ScheduleDevelopmentKit.LegacyCore.Services
{
    public class RoomService : IService<Room>
    {
        private readonly UnitOfWork _uof;

        public RoomService(UnitOfWork uof)
        {
            _uof = uof;
        }

        public Guid Create(string name, Campus campus)
        {
            if (_uof.Rooms.Get().Any(r => r.Name == name && r.Campus == campus))
                throw new Exception("The Room already exists");

            var result = new Room() { Name = name, Campus = campus };
            _uof.Rooms.Create(result);
            return result.Id;
        }

        public Room FindById(Guid id)
        {
            return _uof.Rooms.FindById(id);
        }

        public IEnumerable<Room> Get()
        {
            return _uof.Rooms.Get();
        }

        public void Remove(Guid roomId)
        {
            _uof.Rooms.Remove(_uof.Rooms.FindById(roomId));
        }

        public void Update(Room room)
        {
            _uof.Rooms.Update(room);
        }
    }
}
