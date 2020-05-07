using System;
using System.Threading.Tasks;
using FandNCloud.Services.BasketActivities.Domain.Models;
using FandNCloud.Services.BasketActivities.Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace FandNCloud.Services.BasketActivities.Repositories
{
    public class BasketBlockRepository : IBasketBlockRepository
    {
        private readonly IMongoDatabase _database;
        
        public BasketBlockRepository(IMongoDatabase database)
        {
            _database = database;
        }
        
        public async Task<BasketBlock> GetAsync(Guid id)
            => await Collection.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        
        
        public async Task AddAsync(BasketBlock basketBlock)
            => await Collection.InsertOneAsync(basketBlock);
        
        public async Task RemoveAsync(Guid id)
            => await Collection.DeleteOneAsync(x => x.Id == id);
        
        private IMongoCollection<BasketBlock> Collection 
            => _database.GetCollection<BasketBlock>("BasketBlocks");  
    }
}