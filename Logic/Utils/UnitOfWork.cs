using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Logic.Utils
{
    public sealed class UnitOfWork
    {
        private readonly DbContext _dbContext;
        private readonly IDbContextTransaction _transaction;

        public UnitOfWork(DbContext dbContext)
        {           
            _dbContext = dbContext;
            _transaction = _dbContext.Database.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            finally
            {
                _transaction.Dispose();
                _dbContext.Dispose();
            }

        }

        internal async Task<TEntity> GetAsync<TEntity, TKey>(TKey id)
            where TEntity : class, IEntity<TKey>
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        internal void Save<TEntity>(TEntity entity)
            where TEntity : class
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        internal void Update<TEntity>(TEntity entity)
            where TEntity : class
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        internal void Delete<TEntity>(TEntity entity)
            where TEntity : class
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public IQueryable<TEntity> Query<TEntity>()
            where TEntity : class
        {
            return _dbContext.Set<TEntity>();
        }
    }
}
