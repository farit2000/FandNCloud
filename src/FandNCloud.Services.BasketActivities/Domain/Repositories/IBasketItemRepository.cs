using System.Collections.Generic;
using System.Threading.Tasks;
using FandNCloud.Services.BasketActivities.Domain.Models;

namespace FandNCloud.Services.BasketActivities.Domain.Repositories
{
    public interface IBasketItemRepository
    {
        Task<BasketItem> GetAsync(string path, string name);
        Task<IEnumerable<BasketItem>> BrowseAsync(string path);
        Task AddAsync(BasketItem item);
        Task DeleteAsync(string path, string name);
        Task RenameAsync(string path, string name, string newName);
        Task ReplaceAsync(string path, string name, string newPath);
    }
}