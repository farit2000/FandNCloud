using System;
using System.Threading.Tasks;
using FandNCloud.Common.RestEasy;
using FandNCloud.Services.BasketActivities.Services;
using Microsoft.AspNetCore.Mvc;

namespace FandNCloud.Services.BasketActivities.Controllers
{
    [Route("[controller]")]
    public class BasketActivitiesController : Controller
    {
        private readonly IBasketBlockService _blockService;
        private readonly IFileService _fileService;
        private readonly IFolderService _folderService;

        public BasketActivitiesController(IBasketBlockService userService, IFileService fileService,
            IFolderService folderService)
        {
            _blockService = userService;
            _folderService = folderService;
            _fileService = fileService;
        }
        
        [HttpGet("file/{userId}/{path}/{name}")]
        public async Task<ActionResult<BasketFileMessageModel>> Get_File(string path, string name, Guid userId)
        {
            var file = await _fileService.GetAsync(userId, path, name);
            // var product = await _blockService.GetAsync(path);
            if (file == null)
            {
                return NotFound();
            }
            return new ActionResult<BasketFileMessageModel>(new BasketFileMessageModel(file.Name));
        }

        [HttpGet("folder/{userId}/{path}/{name}")]
        public async Task<ActionResult<BasketFolderMessageModel>> Get_Folder(string path, string name, Guid userId)
        {
            var folder = await _folderService.GetAsync(userId, path, name);
            // var product = await _blockService.GetAsync(path);
            if (folder == null)
            {
                return NotFound();
            }
            return new ActionResult<BasketFolderMessageModel>(new BasketFolderMessageModel(folder.Name));
        }
    }
}