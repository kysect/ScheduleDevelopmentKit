using ScheduleAggregator.DataModels.Entities;
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
        private UnitOfWork UOF;
        public RoomService(UnitOfWork uof)
        {
            UOF = uof;
        }
        public void Create(string name)
        {
            if (UOF.Rooms.Get(_ => _.Name == name) != null)
                throw new Exception("The Room already exists");

            UOF.Rooms.Create(new Room() { Name = name });
        }
        public void Update(Room room)
        {
            UOF.Rooms.Update(room);
        }

        ///

        public Room FindByID(int id)
        {
            return UOF.Rooms.FindById(id);
        }

        public IEnumerable<Room> Get()
        {
            return UOF.Rooms.Get();
        }
        public void Remove(Room room)
        {
            UOF.Rooms.Remove(room);
        }
    }
}
