using System;
using System.Linq;

namespace ScheduleAggregator.DataModels.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity Create(TEntity item);
        TEntity FindById(Guid id);
        IQueryable<TEntity> Get();
        //TODO: change signature
        IQueryable<TEntity> Get(Func<TEntity, bool> predicate);
        void Remove(TEntity item);
        void Update(TEntity item);
    }
}
