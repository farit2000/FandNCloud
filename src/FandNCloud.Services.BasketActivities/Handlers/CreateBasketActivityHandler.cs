using System;
using System.Threading.Tasks;
using FandNCloud.Common.Commands;
using FandNCloud.Common.Events;
using FandNCloud.Services.BasketActivities.Services;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace FandNCloud.Services.BasketActivities.Handlers
{
    public class CreateBasketActivityHandler : ICommandHandler<CreateBasketActivity>
    {
        private IBusClient _busClient;
        private IBasketBlockService _basketBlockService;

        public CreateBasketActivityHandler(IBusClient busClient, IBasketBlockService basketBlockService)
        {
            _busClient = busClient;
            _basketBlockService = basketBlockService;
        }
        
        public async Task HandleAsync(CreateBasketActivity command)
        {
            Console.WriteLine($"Creating basket activity: {command.ItemName}" + " " + $"Time is: {DateTime.Now}" + " " + $"Microsec: {DateTime.Now.Millisecond}");
            await _busClient.PublishAsync(new BasketActivityCreated(command.Id, command.UserId,
                command.ItemName, command.ItemType, command.ItemPath, command.ItemExtension, DateTime.Now));
        }
    }
}