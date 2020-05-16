using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FandNCloud.Services.BasketActivities.Domain.Models;

namespace FandNCloud.Services.BasketActivities.Services
{
    public interface IFolderService
    {
        Task AddAsync(Guid userId, BasketFolder folder);
        Task<BasketFolder> GetAsync(Guid userId, string path, string name);
        Task RemoveAsync(Guid userId, string path, string name);

        Task<Tuple<List<BasketFolder>, List<BasketFile>>> BrowseFolder(Guid userId, string path, string name);
    }
}