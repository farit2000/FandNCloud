using System;
using System.Threading.Tasks;
using FandNCloud.Common.Commands;
using FandNCloud.Common.Events;
using FandNCloud.Services.BasketActivities.Services;
using Microsoft.Extensions.Logging;

namespace FandNCloud.Services.BasketActivities.Handlers
{
    public class CreateUserHandler : IEventHandler<UserCreated>
    {
        private readonly IBasketBlockService _basketBlockService;
        private readonly IBlobService _blobService;
        private readonly ILogger _logger;

        public CreateUserHandler(IBasketBlockService basketBlockService, IBlobService blobService,
            ILogger<CreateUserHandler> logger)
        {
            _basketBlockService = basketBlockService;
            _blobService = blobService;
            _logger = logger;
        }

        public async Task HandleAsync(UserCreated @event)
        {
            await _basketBlockService.AddAsync(@event.UserId, @event.Email, @event.UserId.ToString());
            await _blobService.AddNewBlobContainerAsync(@event.UserId.ToString());
        }
    }
}