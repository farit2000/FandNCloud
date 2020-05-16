using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FandNCloud.Services.BasketActivities.Domain.Models;
using FandNCloud.Services.BasketActivities.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace FandNCloud.Services.BasketActivities.Repositories
{
    public class FolderRepository : IBasketFolderRepository
    {
        private readonly IMongoDatabase _database;

        public FolderRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<BasketFolder> GetAsync(string path, string name, Guid userId)
        {
            var block = await Collection.AsQueryable()
                .FirstOrDefaultAsync(x => x.UserId == userId);
            return block.Folders.FirstOrDefault(x => x.Name == name && x.Path == path);
        }
        
        public async Task<IEnumerable<BasketFile>> BrowseAllFiles(string path, string name, Guid userId)
        {
            var p = "";
            if (path == null)
                p = "," + name + ",";
            else
                p = path + name + ",";
            var block = await Collection.AsQueryable()
                .FirstOrDefaultAsync(x => x.UserId == userId);
            var res = block.Files.FindAll(x => x.Path == p).AsEnumerable();
            return res;
        }

        public async Task<IEnumerable<BasketFolder>> BrowseAllFolders(string path, string name, Guid userId)
        {
            var p = "";
            if (path == null)
                p = "," + name + ",";
            else
                p = path + name + ",";
            var block = await Collection.AsQueryable()
                .FirstOrDefaultAsync(x => x.UserId == userId);
            var res = block.Folders.FindAll(x => x.Path == p).AsEnumerable();
            return res;
        }

        public async Task AddAsync(BasketFolder folder, Guid userId)
        {
            await Collection.UpdateOneAsync(x => x.UserId == userId,
                Builders<BasketBlock>.Update.AddToSet(s => s.Folders, folder));
        }

        public async Task RemoveAsync(string path, string name, Guid userId)
        {
            await Collection.UpdateOneAsync(x => x.UserId == userId,
                Builders<BasketBlock>.Update
                    .PullFilter(s => s.Folders,
                        f => f.Name == name && f.Path == path));
        }
        
        private IMongoCollection<BasketBlock> Collection 
            => _database.GetCollection<BasketBlock>("BasketBlocks");  
    }
}