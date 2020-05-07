using System;
using System.Threading.Tasks;
using FandNCloud.Api.Handlers;
using FandNCloud.Common.Commands;
using FandNCloud.Common.Events;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace FandNCloud.Api.Controllers
{
    [Route("[controller]")]
    public class BasketActivityController : Controller
    {
        private readonly IBusClient _busClient;
        
        public BasketActivityController(IBusClient busClient)
        {
            _busClient = busClient;
        }
        
        [HttpGet("get")]
        public IActionResult Get() => Content("Hello world!");

        [HttpPost("post")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm]CreateBasketActivity command)
        {
            command.Id = Guid.NewGuid(); 
            await _busClient.PublishAsync(command);
            return Accepted($"basketactivity/{command.Id}");
        }
    }
}