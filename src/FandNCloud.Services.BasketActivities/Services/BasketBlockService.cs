using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FandNCloud.Services.BasketActivities.Domain.Models;
using FandNCloud.Services.BasketActivities.Domain.Repositories;

namespace FandNCloud.Services.BasketActivities.Services
{
    public class BasketBlockService : IBasketBlockService
    {
        private readonly IBasketBlockRepository _basketBlockRepository;
        private readonly IBasketItemRepository _basketItemRepository;

        public BasketBlockService(IBasketBlockRepository basketBlockRepository,
            IBasketItemRepository basketItemRepository)
        {
            _basketBlockRepository = basketBlockRepository;
            _basketItemRepository = basketItemRepository;
        }
        
        public async Task AddAsync(Guid id, Guid userId, string userEmail, List<BasketItem> items)
        {
            var basketBlock = new BasketBlock(id, userId, userEmail, items);
            await _basketBlockRepository.AddAsync(basketBlock);
        }

        public async Task AddAsync(Guid id, Guid userId, string userEmail)
        {
            var basketBlock = new BasketBlock(id, userId, userEmail);
            await _basketBlockRepository.AddAsync(basketBlock);
        }
    }
}