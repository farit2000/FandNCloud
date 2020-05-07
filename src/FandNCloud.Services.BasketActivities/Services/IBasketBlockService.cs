using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FandNCloud.Services.BasketActivities.Domain.Models;

namespace FandNCloud.Services.BasketActivities.Services
{
    public interface IBasketBlockService
    {
        Task AddAsync(Guid id, Guid userId, string userEmail, List<BasketItem> items);
        Task AddAsync(Guid id, Guid userId, string userEmail);
    }
}