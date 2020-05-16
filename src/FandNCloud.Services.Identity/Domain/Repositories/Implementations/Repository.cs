using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FandNCloud.Common.Repositories;
using FandNCloud.Services.Identity.Domain.Database;
using Microsoft.EntityFrameworkCore;

namespace FandNCloud.Services.Identity.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity: class
    {
        protected readonly Func<IdentityContext> ContextFactory;
        
        public Repository(Func<IdentityContext> contextFactory)
        {
            ContextFactory = contextFactory;
        }
        
        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            using (var context = ContextFactory())
            {
                return await context.Set<TEntity>().FindAsync(id);
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            using (var context = ContextFactory())
            {
                return await context.Set<TEntity>().ToListAsync();
            }
        }

        public async Task AddAsync(TEntity entity)
        {
            using (var context = ContextFactory())
            {
                context.Set<TEntity>().Add(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(TEntity entity)
        {
            using (var context = ContextFactory())
            {
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(TEntity entity)
        {
            using (var context = ContextFactory())
            {
                context.Set<TEntity>().Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task<bool> ContainsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            using (var context = ContextFactory())
            {
                var item = await context.Set<TEntity>().FirstOrDefaultAsync(predicate);
                if (item == null)
                    return false;
                return true;
            }
        }
    }
}