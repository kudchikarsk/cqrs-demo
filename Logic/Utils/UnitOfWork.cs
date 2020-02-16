using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Logic.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Logic.Utils
{
    public sealed class UnitOfWork
    {
        private readonly DbContext _dbContext;
        private readonly IDbContextTransaction _transaction;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _transaction = _dbContext.Database.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            try
            {
                 await _dbContext.SaveChangesAsync();
                 await _transaction.CommitAsync();
            }
            finally
            {
                _transaction.Dispose();
                _dbContext.Dispose();
            }

        }

        public void Add<TEntity>(TEntity entity)
            where TEntity : class
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void Update<TEntity>(TEntity entity)
            where TEntity : class
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        public void Delete<TEntity>(TEntity entity)
            where TEntity : class
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public DbSet<TEntity> Query<TEntity>()
            where TEntity : class
        {
            return _dbContext.Set<TEntity>();
        }
    }
}
