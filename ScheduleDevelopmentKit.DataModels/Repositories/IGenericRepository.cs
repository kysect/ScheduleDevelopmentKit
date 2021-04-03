using System;
using System.Linq;

namespace ScheduleDevelopmentKit.DataModels.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity Create(TEntity item);
        TEntity FindById(Guid id);
        IQueryable<TEntity> Get();
        void Remove(TEntity item);
        void Update(TEntity item);
    }
}
