using System;
using System.Threading.Tasks;
using FandNCloud.Common.Commands;
using FandNCloud.Common.Events;
using FandNCloud.Common.Exceptions;
using FandNCloud.Services.Identity.Domain.Database;
using FandNCloud.Services.Identity.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace FandNCloud.Services.Identity.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly ILogger _logger;
        private readonly IBusClient _busClient;
        private readonly IUserService _userService;

        public CreateUserHandler(IBusClient busClient,
            IUserService userService, 
            ILogger<CreateUser> logger)
        {
            _busClient = busClient;
            _userService = userService;
            _logger = logger;
        }

        public async Task HandleAsync(CreateUser command)
        {
            _logger.LogInformation($"Creating user: '{command.Email}' with firstname: '{command.FirstName}'.");
            try 
            {
                await _userService.RegisterAsync(command.Email, command.Password, command.FirstName, command.LastName);
                // UserCreated should be published once user has been created
                await _busClient.PublishAsync(new UserCreated(command.Email, command.FirstName, command.LastName));
                _logger.LogInformation($"User: '{command.Email}' was created with name: '{command.FirstName}'.");
            }
            catch (ActioException ex)
            {
                _logger.LogError(ex, ex.Message);
                await _busClient.PublishAsync(new CreateUserRejected(command.Email,
                    ex.Message, ex.Code)); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await _busClient.PublishAsync(new CreateUserRejected(command.Email,
                    ex.Message, "error"));                
            }
        }
    }
}