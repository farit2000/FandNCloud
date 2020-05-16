using System.Threading.Tasks;
using FandNCloud.Common.Commands;
using Microsoft.Extensions.Logging;

namespace FandNCloud.Services.BasketActivities.Handlers
{
    public class DeleteFolderHandler : ICommandHandler<DeleteFolder>
    {
        private ILogger _logger;

        public DeleteFolderHandler(ILogger<DeleteFolderHandler> logger)
        {
            _logger = logger;
        }
        public Task HandleAsync(DeleteFolder command)
        {
            throw new System.NotImplementedException();
        }
    }
}