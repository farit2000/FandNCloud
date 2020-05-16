using System;
using System.Threading.Tasks;
using FandNCloud.Common.Commands;
using FandNCloud.Common.Exceptions;
using FandNCloud.Services.BasketActivities.Domain.Models;
using FandNCloud.Services.BasketActivities.Services;
using Microsoft.Extensions.Logging;

namespace FandNCloud.Services.BasketActivities.Handlers
{
    public class CreateFileHandler : ICommandHandler<CreateFile>
    {
        private readonly IFileService _fileService;
        private readonly ILogger _logger;
        
        public CreateFileHandler(IFileService fileService, ILogger<CreateFileHandler> logger)
        {
            _fileService = fileService;
            _logger = logger;
        }
        public async Task HandleAsync(CreateFile command)
        {
            _logger.LogInformation($"Creating file: '{command.Id}' for user: '{command.UserId}'.");
            try
            {
                await _fileService.AddAsync(command.UserId,
                    new BasketFile(command.Name, command.Path, command.Extension));
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