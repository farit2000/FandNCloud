using System;
using System.Threading.Tasks;
using FandNCloud.Services.BasketActivities.Domain.Models;

namespace FandNCloud.Services.BasketActivities.Domain.Repositories
{
    public interface IBasketBlockRepository
    {
        Task<BasketBlock> GetAsync(Guid userId);
        Task<BasketBlock> GetAsync(string userEmail);
        Task AddAsync(BasketBlock basketBlock);
        Task RemoveAsync(Guid userId);
    }
}