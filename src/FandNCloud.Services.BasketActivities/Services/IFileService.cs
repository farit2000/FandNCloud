using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FandNCloud.Services.BasketActivities.Domain.Models;

namespace FandNCloud.Services.BasketActivities.Services
{
    public interface IFileService
    {
        Task AddAsync(Guid userId, BasketFile file);
        Task<BasketFile> GetAsync(Guid userId, string path, string name);
        Task RemoveAsync(Guid userId, string path, string name);
    }
}