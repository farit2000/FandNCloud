using System;
using System.Threading.Tasks;
using FandNCloud.Common.Events;
using FandNCloud.Common.Exceptions;
using FandNCloud.Common.Requests;
using FandNCloud.Common.Responds;
using FandNCloud.Services.Identity.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace FandNCloud.Services.Identity.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest>
    {
        private readonly ILogger _logger;
        private readonly IBusClient _busClient;
        private readonly IUserService _userService;

        public CreateUserHandler(IBusClient busClient,
            IUserService userService, 
            ILogger<CreateUserRequest> logger)
        {
            _busClient = busClient;
            _userService = userService;
            _logger = logger;
        }

        public async Task<IRespond> HandleAsync(CreateUserRequest request)
        {
            _logger.LogInformation($"Creating user: '{request.Email}' with firstname: '{request.FirstName}'.");
            try 
            {
                var user = await _userService.RegisterAsync(request.Email, request.Password, request.FirstName, request.LastName);
                // UserCreated should be published once user has been created
                await _busClient.PublishAsync(new UserCreated(user.Id, request.Email, request.FirstName, request.LastName));
                _logger.LogInformation($"User: '{request.Email}' was created with name: '{request.FirstName}'.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new CreateUserRespond(new BadRequestResult());
            }

            return new CreateUserRespond(new AcceptedResult());
        }
    }
}