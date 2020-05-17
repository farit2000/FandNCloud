using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FandNCloud.Services.BasketActivities.Domain.Models;

namespace FandNCloud.Services.BasketActivities.Domain.Repositories
{
    public interface IBasketFolderRepository
    {
        Task<BasketFolder> GetAsync(string path, string name, Guid userId);
        Task<IEnumerable<BasketFile>> BrowseAllFiles(string path, string name, Guid userId);
        Task<IEnumerable<BasketFolder>> BrowseAllFolders(string path, string name, Guid userId);
        Task AddAsync(BasketFolder folder, Guid userId);
        Task RemoveAsync(string path, string name, Guid userId);
    }
}