using ScheduleAggregator.DataModels.Entities;
using ScheduleAggregator.DataModels.Enums;
using ScheduleAggregator.DataModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleAggregator.DataModels.Services
{
    public class RoomService
    {
        private UnitOfWork _uof;
        public RoomService(UnitOfWork uof)
        {
            _uof = uof;
        }
        public Guid Create(string name, Campus campus)
        {
            if (_uof.Rooms.Get(el => el.Name == name && el.Campus == campus).Any())
                throw new Exception("The Room already exists");

            var Out = new Room() { Name = name, Campus = campus };
            _uof.Rooms.Create(Out);
            return Out.Id;
        }

        #region SameOperations

        public Room FindByID(Guid id)
        {
            return _uof.Rooms.FindById(id);
        }

        public IEnumerable<Room> Get()
        {
            return _uof.Rooms.Get();
        }
        public IEnumerable<Room> Get(Func<Room, bool> predicate)
        {
            return _uof.Rooms.Get(predicate);
        }
        public void Remove(Guid roomID)
        {
            _uof.Rooms.Remove(_uof.Rooms.FindById(roomID));
        }
        public void Update(Room room)
        {
            _uof.Rooms.Update(room);
        }
        #endregion
    }
}
