using System.Threading.Tasks;
using FandNCloud.Common.Commands;
using Microsoft.Extensions.Logging;

namespace FandNCloud.Services.BasketActivities.Handlers
{
    public class DeleteFileHandler : ICommandHandler<DeleteFile>
    {
        private readonly ILogger _logger;

        public DeleteFileHandler(ILogger<DeleteFileHandler> logger)
        {
            _logger = logger;
        }
        public Task HandleAsync(DeleteFile command)
        {
            throw new System.NotImplementedException();
        }
    }
}