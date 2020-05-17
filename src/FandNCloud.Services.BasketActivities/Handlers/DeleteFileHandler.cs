using System;
using System.Threading.Tasks;
using FandNCloud.Common.Commands;
using FandNCloud.Common.Exceptions;
using FandNCloud.Services.BasketActivities.Services;
using Microsoft.Extensions.Logging;

namespace FandNCloud.Services.BasketActivities.Handlers
{
    public class DeleteFileHandler : ICommandHandler<DeleteFile>
    {
        private readonly ILogger _logger;
        private readonly IFileService _fileServicee;

        public DeleteFileHandler(ILogger<DeleteFileHandler> logger, IFileService fileService)
        {
            _logger = logger;
            _fileServicee = fileService;
        }
        public async Task HandleAsync(DeleteFile command)
        {
            _logger.LogInformation($"Deleting file: '{command.Id}' for user: '{command.UserId}'.");
            try
            {
                await _fileServicee.RemoveAsync(command.UserId, command.Path, command.Name);
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