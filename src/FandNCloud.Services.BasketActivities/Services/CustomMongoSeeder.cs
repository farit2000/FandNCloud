using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FandNCloud.Common.Mongo;
using FandNCloud.Services.BasketActivities.Domain.Models;
using FandNCloud.Services.BasketActivities.Domain.Repositories;
using MongoDB.Driver;

namespace FandNCloud.Services.BasketActivities.Services
{
    public class CustomMongoSeeder : MongoSeeder
    {
        private readonly IBasketBlockRepository _basketBlockRepository;
        
        public CustomMongoSeeder(IMongoDatabase database, IBasketBlockRepository basketBlockRepository) : base(database)
        {
            _basketBlockRepository = basketBlockRepository;
        }

        protected override async Task CustomSeedAsync()
        {
            var userId = Guid.NewGuid();
            var items = new List<BasketBlock>
            {
                new BasketBlock(Guid.NewGuid(), userId, "test1@ya.ru", new List<BasketItem>
                {
                    new BasketItem("home", "Folder", null, null),
                    new BasketItem("downloads", "Folder", ",home,", null),
                    new BasketItem("books", "Folder", ",home,", null),
                    new BasketItem("films", "Folder", ",home,downloads,", null),
                    new BasketItem("games", "Folder", ",home,downloads,", null),
                    new BasketItem("gta", "File", ",home,downloads,games,", ".exe"),
                    new BasketItem("c#", "File", ",home,books,", ".pdf")
                })
            };
            await Task.WhenAll(items.Select(x => _basketBlockRepository
                .AddAsync(x)));
        }
    }
}