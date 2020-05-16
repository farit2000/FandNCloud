using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FandNCloud.Common.Repositories
{
    public interface IRepository<TEntity, TKey> where TEntity: class
    {
        Task<TEntity> GetByIdAsync(TKey id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);

        Task<bool> ContainsAsync(Expression<Func<TEntity, bool>> predicate);
    }
}