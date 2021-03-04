using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Payment.Interfaces;
using Payment.Interfaces.IRepositories;

namespace Payment.Repository.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {

        #region Protected

        /// <summary>
        /// Primary database set
        /// </summary>
        protected abstract DbSet<TEntity> DbSet { get; }

        #endregion
        #region Constructor

        protected BaseRepository(IPaymentDbContext context)
        {
            Db = context;
        }

        #endregion

        #region public

        public IPaymentDbContext Db;

        public async Task AddAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public IQueryable<TEntity> GetAllAsync()
        {
            return DbSet;
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Db.SaveChangesAsync();

        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.SingleOrDefaultAsync(predicate);
        }

        #endregion
    }
}
