﻿using System;
using System.Data.Entity;
using System.Linq;

namespace ScheduleAggregator.DataModels.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext context, DbSet<TEntity> set)
        {
            _context = context;
            _dbSet = set;
        }

        public IQueryable<TEntity> Get()
        {
            return _dbSet;
        }

        public TEntity FindById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public TEntity Create(TEntity item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
            return item;
        }

        public void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Remove(TEntity item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }
    }
}
