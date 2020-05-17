using System;
using System.Threading.Tasks;
using FandNCloud.Services.BasketActivities.Domain.Models;
using FandNCloud.Services.BasketActivities.Domain.Repositories;
using FandNCloud.Services.BasketActivities.Repositories;

namespace FandNCloud.Services.BasketActivities.Services
{
    public class FileService : IFileService
    {
        private readonly IBasketFileRepository _basketFileRepository;

        public FileService(IBasketFileRepository basketFileRepository)
        {
            _basketFileRepository = basketFileRepository;
        }
        
        public async Task AddAsync(Guid userId, BasketFile file)
        { 
            await _basketFileRepository.AddAsync(file, userId);
        }

        public async Task<BasketFile> GetAsync(Guid userId, string path, string name)
        {
            return await _basketFileRepository.GetAsync(path, name, userId);
        }

        public async Task RemoveAsync(Guid userId, string path, string name)
        {
            await _basketFileRepository.RemoveAsync(path, name, userId);
        }
    }
}