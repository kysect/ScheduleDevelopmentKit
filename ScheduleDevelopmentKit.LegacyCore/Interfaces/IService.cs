using System;
using System.Collections.Generic;

namespace ScheduleDevelopmentKit.LegacyCore.Interfaces
{
    public interface IService<T> where T : class
    {
        public T FindById(Guid id);
        public IEnumerable<T> Get();
        public void Remove(Guid id);
        public void Update(T entity);
    }
}