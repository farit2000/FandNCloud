using System.Linq;
using System.Threading.Tasks;
using FandNCloud.Common.Requests;
using FandNCloud.Common.Responds;
using FandNCloud.Common.Responds.HelperModels;
using FandNCloud.Services.BasketActivities.Services;

namespace FandNCloud.Services.BasketActivities.Handlers
{
    public class BrowseFolderHandler : IRequestHandler<BrowseFolderRequest>
    {
        private readonly IFolderService _folderService;
        
        public BrowseFolderHandler(IFolderService folderService)
        {
            _folderService = folderService;
        }
        public async Task<IRespond> HandleAsync(BrowseFolderRequest request)
        {
            var filesAndFolders = await _folderService.BrowseFolder(request.UserId,
                request.Path, request.Name);
            var files = filesAndFolders.Item2;
            var folders = filesAndFolders.Item1;

            return new BrowseFolderRespond(folders.Select(e =>
                    new FolderInfo(e.Id, e.Name, e.Path, e.CreatedDate)).ToList(),
                files.Select(e => new FileInfo(e.Id, e.Name, e.Extension, e.Path, e.CreatedDate)).ToList());
        }
    }
}