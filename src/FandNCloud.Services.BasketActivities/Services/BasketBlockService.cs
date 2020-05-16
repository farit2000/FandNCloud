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
        // private readonly IBasketItemRepository _basketItemRepository;

        public BasketBlockService(IBasketBlockRepository basketBlockRepository)
        {
            _basketBlockRepository = basketBlockRepository;
        }
        
        public async Task AddAsync(Guid userId, string userEmail, string containerName, List<BasketFile> files,
            List<BasketFolder> folders)
        {
            var basketBlock = new BasketBlock(userId, userEmail, containerName, files, folders);
            await _basketBlockRepository.AddAsync(basketBlock);
        }

        public async Task AddAsync(Guid userId, string userEmail, string containerName)
        {
            var basketBlock = new BasketBlock(userId, userEmail, containerName);
            await _basketBlockRepository.AddAsync(basketBlock);
        }

        public Task<BasketBlock> GetAsync(Guid userId)
        {
            return _basketBlockRepository.GetAsync(userId);
        }
    }
}