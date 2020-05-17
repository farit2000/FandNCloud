using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FandNCloud.Services.BasketActivities.Domain.Models;
using FandNCloud.Services.BasketActivities.Domain.Repositories;

namespace FandNCloud.Services.BasketActivities.Services
{
    public class FolderService : IFolderService
    {
        private readonly IBasketFolderRepository _basketFolderRepository;

        public FolderService(IBasketFolderRepository basketFolderRepository)
        {
            _basketFolderRepository = basketFolderRepository;
        }
        
        public async Task AddAsync(Guid userId, BasketFolder folder)
        {
            await _basketFolderRepository.AddAsync(folder, userId);
        }

        public async Task<BasketFolder> GetAsync(Guid userId, string path, string name)
        {
            return await _basketFolderRepository.GetAsync(path, name, userId);
        }

        public async Task RemoveAsync(Guid userId, string path, string name)
        {
            await _basketFolderRepository.RemoveAsync(path, name, userId);
        }

        public async Task<Tuple<List<BasketFolder>, List<BasketFile>>> BrowseFolder(Guid userId, string path, string name)
        {
            var files = await _basketFolderRepository.BrowseAllFiles(path, name, userId);
            var folders = await _basketFolderRepository.BrowseAllFolders(path, name, userId);
            return new Tuple<List<BasketFolder>, List<BasketFile>>(folders.ToList(), files.ToList());
        }
    }
}