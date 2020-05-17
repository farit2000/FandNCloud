using System;
using System.Threading.Tasks;
using FandNCloud.Common.Commands;
using FandNCloud.Common.Exceptions;
using FandNCloud.Services.BasketActivities.Domain.Models;
using FandNCloud.Services.BasketActivities.Services;
using Microsoft.Extensions.Logging;

namespace FandNCloud.Services.BasketActivities.Handlers
{
    public class CreateFolderHandler : ICommandHandler<CreateFolder>
    {
        private readonly IFolderService _folderService;
        private readonly ILogger _logger;

        public CreateFolderHandler(IFolderService folderService, ILogger<CreateFolderHandler> logger)
        {
            _folderService = folderService;
            _logger = logger;
        }
        public async Task HandleAsync(CreateFolder command)
        {
            _logger.LogInformation($"Creating folder: '{command.Id}' for user: '{command.UserId}'.");
            try
            {
                await _folderService.AddAsync(command.UserId,
                    new BasketFolder(command.Name, command.Path));
            }
            catch (ActioException e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }
    }
}