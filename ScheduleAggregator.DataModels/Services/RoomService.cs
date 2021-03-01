using ScheduleAggregator.DataModels.Entities;
using ScheduleAggregator.DataModels.Enums;
using ScheduleAggregator.DataModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleAggregator.DataModels.Services
{
    public class RoomService
    {
        private UnitOfWork _uof;
        public RoomService(UnitOfWork uof)
        {
            _uof = uof;
        }
        public Room Create(string name, Campus campus)
        {
            var Out = new Room() { Name = name, Campus = campus };
            _uof.Rooms.Create(Out);
            return Out;
        }
        public void Update(Room room)
        {
            _uof.Rooms.Update(room);
        }

        #region SameOperations

        public Room FindByID(int id)
        {
            return _uof.Rooms.FindById(id);
        }

        public IEnumerable<Room> Get()
        {
            return _uof.Rooms.Get();
        }
        public void Remove(Room room)
        {
            _uof.Rooms.Remove(room);
        }
        #endregion
    }
}
