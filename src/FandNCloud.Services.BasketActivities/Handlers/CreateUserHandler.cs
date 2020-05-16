using System;
using System.Threading.Tasks;
using FandNCloud.Common.Commands;
using FandNCloud.Services.BasketActivities.Services;
using Microsoft.Extensions.Logging;

namespace FandNCloud.Services.BasketActivities.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
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
        public async Task HandleAsync(CreateUser command)
        {
            var userId = Guid.NewGuid();
            await _basketBlockService.AddAsync(userId, command.Email, userId.ToString());
            await _blobService.AddNewBlobAsync(userId.ToString());
        }
    }
}