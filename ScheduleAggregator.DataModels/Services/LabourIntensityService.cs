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
        private UnitOfWork UOF;
        public LabourIntensityService(UnitOfWork uof)
        {
            UOF = uof;
        }
        public void Create(string name)
        {
            UOF.LabourIntensities.Create(new LabourIntensity());
        }
        public void Update(LabourIntensity labourIntensity)
        {
            UOF.LabourIntensities.Update(labourIntensity);
        }

        ///

        public LabourIntensity FindByID(int id)
        {
            return UOF.LabourIntensities.FindById(id);
        }

        public IEnumerable<LabourIntensity> Get()
        {
            return UOF.LabourIntensities.Get();
        }
        public void Remove(LabourIntensity labourIntensity)
        {
            UOF.LabourIntensities.Remove(labourIntensity);
        }
    }
}
