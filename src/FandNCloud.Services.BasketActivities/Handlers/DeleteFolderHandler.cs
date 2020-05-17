using System;
using System.Threading.Tasks;
using FandNCloud.Common.Commands;
using FandNCloud.Common.Exceptions;
using FandNCloud.Services.BasketActivities.Domain.Models;
using FandNCloud.Services.BasketActivities.Services;
using Microsoft.Extensions.Logging;

namespace FandNCloud.Services.BasketActivities.Handlers
{
    public class DeleteFolderHandler : ICommandHandler<DeleteFolder>
    {
        private readonly ILogger _logger;
        private readonly IFolderService _folderService;

        public DeleteFolderHandler(ILogger<DeleteFolderHandler> logger, IFolderService folderService)
        {
            _logger = logger;
            _folderService = folderService;
        }
        public async Task HandleAsync(DeleteFolder command)
        {
            _logger.LogInformation($"Deleting folder: '{command.Id}' for user: '{command.UserId}'.");
            try
            {
                await _folderService.RemoveAsync(command.UserId, command.Path, command.Name);
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