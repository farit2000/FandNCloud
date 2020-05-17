using System;
using System.Threading.Tasks;
using FandNCloud.Services.BasketActivities.Domain.Models;
using FandNCloud.Services.BasketActivities.Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace FandNCloud.Services.BasketActivities.Repositories
{
    public class BlockRepository : IBasketBlockRepository
    {
        private readonly IMongoDatabase _database;
        
        public BlockRepository(IMongoDatabase database)
        {
            _database = database;
        }
        
        public async Task<BasketBlock> GetAsync(Guid userId)
            => await Collection.AsQueryable().FirstOrDefaultAsync(x => x.UserId == userId);

        public async Task<BasketBlock> GetAsync(string userEmail)
            => await Collection.AsQueryable().FirstOrDefaultAsync(x => x.UserEmail == userEmail);
        
        public async Task AddAsync(BasketBlock basketBlock)
            => await Collection.InsertOneAsync(basketBlock);
        
        public async Task RemoveAsync(Guid userId)
            => await Collection.DeleteOneAsync(x => x.UserId == userId);
        
        private IMongoCollection<BasketBlock> Collection 
            => _database.GetCollection<BasketBlock>("BasketBlocks");  
    }
}