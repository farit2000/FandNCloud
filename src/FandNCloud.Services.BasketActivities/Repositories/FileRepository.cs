using System;
using System.Linq;
using System.Threading.Tasks;
using FandNCloud.Services.BasketActivities.Domain.Models;
using FandNCloud.Services.BasketActivities.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace FandNCloud.Services.BasketActivities.Repositories
{
    public class FileRepository : IBasketFileRepository
    {
        private readonly IMongoDatabase _database;
        
        public FileRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<BasketFile> GetAsync(string path, string name, Guid userId)
        {
            // var filter = Builders<BasketBlock>.Filter
            //     .ElemMatch(x => x.Files , x =>  x.Path == path && x.Name == name);
            var block = await Collection.AsQueryable()
                .FirstOrDefaultAsync(x => x.UserId == userId);
            return block.Files.FirstOrDefault(x => x.Name == name && x.Path == path);
            // var res = Collection.FindAsync(filter).Result.FirstAsync();
            // return await Collection.FindAsync(filter).Result;
        }

        public async Task AddAsync(BasketFile file, Guid userId)
        {
            await Collection.UpdateOneAsync(x => x.UserId == userId,
                Builders<BasketBlock>.Update.AddToSet(s => s.Files, file));
        }

        public async Task RemoveAsync(string path, string name, Guid userId)
        {
            await Collection.UpdateOneAsync(x => x.UserId == userId,
                Builders<BasketBlock>.Update
                    .PullFilter(s => s.Files,
                        f => f.Name == name && f.Path == path));
        }
        
        private IMongoCollection<BasketBlock> Collection 
            => _database.GetCollection<BasketBlock>("BasketBlocks");  
    }
}