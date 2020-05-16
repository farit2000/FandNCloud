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
            Console.WriteLine(userId.ToString());
            var items = new List<BasketBlock>
            {
                new BasketBlock(userId, "test1@ya.ru", "test" ,
                    new List<BasketFile>
                {
                    new BasketFile("gta", ",home,downloads,games,", ".exe"),
                    new BasketFile("c#", ",home,books,", ".pdf")
                }, 
                new List<BasketFolder>
                {
                    new BasketFolder("home", null),
                    new BasketFolder("downloads", ",home,"),
                    new BasketFolder("books", ",home,"),
                    new BasketFolder("films", ",home,downloads,"),
                    new BasketFolder("games", ",home,downloads,"),
                })
            };
            await Task.WhenAll(items.Select(x => _basketBlockRepository
                .AddAsync(x)));
        }
    }
}