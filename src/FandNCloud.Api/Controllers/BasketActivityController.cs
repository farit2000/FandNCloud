using System;
using System.Threading.Tasks;
using FandNCloud.Api.Handlers;
using FandNCloud.Api.Services;
using FandNCloud.Common.Commands;
using FandNCloud.Common.Events;
using FandNCloud.Common.Requests;
using FandNCloud.Common.Responds;
using FandNCloud.Common.RestEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using RawRabbit.Instantiation;
using RestEase;
using CreateFile = FandNCloud.Common.Commands.CreateFile;

namespace FandNCloud.Api.Controllers
{
    [Route("[controller]")]
    public class BasketActivityController : Controller
    {
        private readonly IBusClient _busClient;
        private readonly IBasketActivitiesService _basketActivitiesService;

        public BasketActivityController(IBusClient busClient, IBasketActivitiesService basketActivitiesService)
        {
            _basketActivitiesService = basketActivitiesService;
            _busClient = busClient;
        }

        [HttpGet("get")]
        public IActionResult Get() => Content("Hello world!");

        // [HttpPost("post")]
        // [Consumes("multipart/form-data")]
        // public async Task<IActionResult> Post([FromForm] CreateBasketActivity command)
        // {
        //     command.Id = Guid.NewGuid();
        //     await _busClient.PublishAsync(command);
        //     return Accepted($"basketactivity/{command.Id}");
        // }

        [HttpPost("create_folder")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> FolderCreate([FromForm] CreateFolder command)
        {
            command.UserId = Guid.Parse("4ba23ff7-bd03-4b7c-a742-70c94cefcee1");
            command.Id = Guid.NewGuid();
            await _busClient.PublishAsync(command);
            return Accepted($"folder/{command.Id}");
        }
        
        [HttpDelete("delete_folder")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> FolderDelete([FromForm] DeleteFolder command)
        {
            command.UserId = Guid.Parse("4ba23ff7-bd03-4b7c-a742-70c94cefcee1");
            command.Id = Guid.NewGuid();
            await _busClient.PublishAsync(command);
            return Accepted($"folder/{command.Id}");
        }

        [HttpPost("create_file")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> FileCreate([FromForm] CreateFile command)
        {
            command.UserId = Guid.Parse("4ba23ff7-bd03-4b7c-a742-70c94cefcee1");
            command.Id = Guid.NewGuid();
            await _busClient.PublishAsync(command);
            return Accepted($"file/{command.Id}");
        }
        
        [HttpDelete("delete_file")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> FileDelete([FromForm] DeleteFile command)
        {
            command.UserId = Guid.Parse("4ba23ff7-bd03-4b7c-a742-70c94cefcee1");
            command.Id = Guid.NewGuid();
            await _busClient.PublishAsync(command);
            return Accepted($"file/{command.Id}");
        }

        [HttpGet("browse_folder")]
        [Consumes("multipart/form-data")]
        public async Task<BrowseFolderRespond> BrowseFolder([FromForm] BrowseFolder command)
        {
            command.UserId = Guid.Parse("19a338b0-decf-4bef-8f4a-e0106588579a");
            var filesAndFolders =
                await _busClient.RequestAsync<BrowseFolderRequest, BrowseFolderRespond>(
                    new BrowseFolderRequest(command.UserId, command.Name, command.Path));
            return filesAndFolders;
        }
        
        // [HttpGet("get_file/{path}/{name}")]
        // [Consumes("multipart/form-data")]
        // public string GetFile(string path, string name)
        // {
        //     var userId = Guid.Parse("19a338b0-decf-4bef-8f4a-e0106588579a");
        //     BasketFileMessageModel item =  _basketActivitiesService.GetFileAsync(path, name, userId).Result;
        //     return item.Name;
        // }

        [HttpGet("get_sas_file_read/{containerName}/{blobName}")]
        public async Task<string> GetSasFileRead(string containerName, string blobName)
        {
            var fileReadingSas =
                await _busClient.RequestAsync<SasFileReadRequest, SasFileReadRespond>(
                    new SasFileReadRequest(containerName, blobName));
            return fileReadingSas.Url;
        }

        [HttpGet("get_sas_file_delete/{containerName}/{blobName}")]
        public async Task<string> GetSasFileDelete(string containerName, string blobName)
        {
            var fileReadingSas =
                await _busClient.RequestAsync<SasFileDeleteRequest, SasFileDeleteRespond>(
                    new SasFileDeleteRequest(containerName, blobName));
            return fileReadingSas.Url;
        }
        
        [HttpGet("get_sas_file_add/{containerName}/{blobName}")]
        public async Task<string> GetSasFileAdd(string containerName, string blobName)
        {
            var fileReadingSas =
                await _busClient.RequestAsync<SasFileAddRequest, SasFileAddRespond>(
                    new SasFileAddRequest(containerName, blobName));
            return fileReadingSas.Url;
        }
    }
}