using System;
using System.Threading.Tasks;
using FandNCloud.Services.BasketActivities.Domain.Models;

namespace FandNCloud.Services.BasketActivities.Domain.Repositories
{
    public interface IBasketFileRepository
    {
        Task<BasketFile> GetAsync(string path, string name, Guid userId);
        Task AddAsync(BasketFile file, Guid userId);
        Task RemoveAsync(string path, string name, Guid userId);
    }
}