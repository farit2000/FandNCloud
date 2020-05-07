using System.Collections.Generic;
using System.Threading.Tasks;
using FandNCloud.Services.BasketActivities.Domain.Models;
using FandNCloud.Services.BasketActivities.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace FandNCloud.Services.BasketActivities.Repositories
{
    public class BasketItemRepository : IBasketItemRepository
    {
        private readonly IMongoDatabase _database;
        
        public BasketItemRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<BasketItem> GetAsync(string path, string name)
            => await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Path == path && x.Name == name);

        public async Task<IEnumerable<BasketItem>> BrowseAsync(string path)
            => await Collection
                .AsQueryable().Where(x => x.Path == path)
                .ToListAsync();

        public async Task AddAsync(BasketItem item)
            => await Collection.InsertOneAsync(item);

        public async Task DeleteAsync(string path, string name)
            => await Collection.DeleteOneAsync(x => (x.Path == path && x.Name == name));

        public async Task RenameAsync(string path, string name, string newName)
            => await Collection.UpdateOneAsync(x => x.Name == name && x.Path == path, newName);

        public async Task ReplaceAsync(string path, string name, string newPath)
        {
            throw new System.NotImplementedException();
        }
        
        private IMongoCollection<BasketItem> Collection 
            => _database.GetCollection<BasketItem>("BasketItems");   
    }
}