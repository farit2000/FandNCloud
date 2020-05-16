using System.Threading.Tasks;
using FandNCloud.Common.Commands;
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
        public Task HandleAsync(CreateFolder command)
        {
            throw new System.NotImplementedException();
        }
    }
}