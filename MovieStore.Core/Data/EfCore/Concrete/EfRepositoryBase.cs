using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Core.Data.EfCore.Concrete
{
    public class EfRepositoryBase<TContext, TEntity> : IEntityRepositoryBase<TEntity> where TEntity : class, IEntity
        where TContext : DbContext, new()
    {
        private readonly TContext _tContext;
        public EfRepositoryBase(TContext tContext)
        {
            _tContext = tContext;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _tContext.AddAsync(entity);
            await _tContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {

            _tContext.Remove(entity);
            await _tContext.SaveChangesAsync();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            return _tContext.Set<TEntity>().Where(filter);
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? _tContext.Set<TEntity>().AsNoTracking() : _tContext.Set<TEntity>().Where(filter).AsNoTracking();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _tContext.Update(entity);
            await _tContext.SaveChangesAsync();
            return entity;
        }

    }
}
