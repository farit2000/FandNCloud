using System;
using System.Threading.Tasks;
using FandNCloud.Api.Attributes;
using FandNCloud.Api.Handlers;
using FandNCloud.Api.Services;
using FandNCloud.Common.Commands;
using FandNCloud.Common.Events;
using FandNCloud.Common.Requests;
using FandNCloud.Common.Responds;
using FandNCloud.Common.RestEasy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using RawRabbit.Instantiation;
using RestEase;

namespace FandNCloud.Api.Controllers
{
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme), JwtAuthorize]
    public class BasketActivityController : Controller
    {
        private readonly IBusClient _busClient;

        public BasketActivityController(IBusClient busClient)
        {
            _busClient = busClient;
        }
        
        [HttpGet("get")]
        public IActionResult Get() => Content(User.Identity.Name);
        
        [HttpPost("create_folder")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> FolderCreate([FromForm] CreateFolder command)
        {
            var st = User.Identity.Name;
            command.UserId = Guid.Parse(User.Identity.Name);
            command.Id = Guid.NewGuid();
            await _busClient.PublishAsync(command);
            return Accepted($"folder/{command.Id}");
        }
        
        [HttpDelete("delete_folder")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> FolderDelete([FromForm] DeleteFolder command)
        {
            command.UserId = Guid.Parse(User.Identity.Name);
            command.Id = Guid.NewGuid();
            await _busClient.PublishAsync(command);
            return Accepted($"folder/{command.Id}");
        }
        
        [HttpPost("create_file")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> FileCreate([FromForm] CreateFile command)
        {
            command.UserId = Guid.Parse(User.Identity.Name);
            command.Id = Guid.NewGuid();
            await _busClient.PublishAsync(command);
            return Accepted($"file/{command.Id}");
        }
        
        [HttpDelete("delete_file")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> FileDelete([FromForm] DeleteFile command)
        {
            command.UserId = Guid.Parse(User.Identity.Name);
            command.Id = Guid.NewGuid();
            await _busClient.PublishAsync(command);
            return Accepted($"file/{command.Id}");
        }
        
        [HttpGet("browse_folder")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<BrowseFolderRespond> BrowseFolder([FromForm] BrowseFolder command)
        {
            command.UserId = Guid.Parse(User.Identity.Name);
            var filesAndFolders =
                await _busClient.RequestAsync<BrowseFolderRequest, BrowseFolderRespond>(
                    new BrowseFolderRequest(command.UserId, command.Name, command.Path));
            return filesAndFolders;
        }
        
        [HttpGet("get_sas_file_read/{blobName}")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<string> GetSasFileRead(string blobName)
        {
            var userId = Guid.Parse(User.Identity.Name);
            var containerName = userId.ToString();
            var fileReadingSas =
                await _busClient.RequestAsync<SasFileReadRequest, SasFileReadRespond>(
                    new SasFileReadRequest(userId, containerName, blobName));
            return fileReadingSas.Url;
        }
        
        [HttpGet("get_sas_file_delete/{blobName}")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<string> GetSasFileDelete(string blobName)
        {
            var userId = Guid.Parse(User.Identity.Name);
            var containerName = userId.ToString();
            var fileReadingSas =
                await _busClient.RequestAsync<SasFileDeleteRequest, SasFileDeleteRespond>(
                    new SasFileDeleteRequest(userId, containerName, blobName));
            return fileReadingSas.Url;
        }
        
        [HttpGet("get_sas_file_add/{blobName}")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<string> GetSasFileAdd(string blobName)
        {
            var userId = Guid.Parse(User.Identity.Name);
            var containerName = userId.ToString();
            var fileReadingSas =
                await _busClient.RequestAsync<SasFileAddRequest, SasFileAddRespond>(
                    new SasFileAddRequest(userId, containerName, blobName));
            return fileReadingSas.Url;
        }
    }
}