using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FandNCloud.Services.BasketActivities.Domain.Models;

namespace FandNCloud.Services.BasketActivities.Services
{
    public interface IBasketBlockService
    {
        Task AddAsync(Guid userId, string userEmail, string containerName, List<BasketFile> files, List<BasketFolder> folders);
        Task AddAsync(Guid userId, string userEmail, string containerName);
        Task<BasketBlock> GetAsync(Guid userId);
        // Task<string> GetAsyncName(string path);
    }
}