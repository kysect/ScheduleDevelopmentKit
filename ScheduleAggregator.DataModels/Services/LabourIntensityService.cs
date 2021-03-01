using ScheduleAggregator.DataModels.Entities;
using ScheduleAggregator.DataModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleAggregator.DataModels.Services
{
    public class LabourIntensityService
    {
        private UnitOfWork _uof;
        public LabourIntensityService(UnitOfWork uof)
        {
            _uof = uof;
        }
        public void Create(string name)
        {
            _uof.LabourIntensities.Create(new LabourIntensity());
        }
        public void Update(LabourIntensity labourIntensity)
        {
            _uof.LabourIntensities.Update(labourIntensity);
        }

        ///

        public LabourIntensity FindByID(int id)
        {
            return _uof.LabourIntensities.FindById(id);
        }

        public IEnumerable<LabourIntensity> Get()
        {
            return _uof.LabourIntensities.Get();
        }
        public void Remove(LabourIntensity labourIntensity)
        {
            _uof.LabourIntensities.Remove(labourIntensity);
        }
    }
}
