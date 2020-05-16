using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FandNCloud.Common.PostgreSQL
{
    public interface IPosgreSqlRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}