using System;
using System.Threading.Tasks;
using FandNCloud.Common.Commands;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace FandNCloud.Api.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IBusClient _busClient;
        
        public UserController(IBusClient busClient)
        {
            _busClient = busClient;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] CreateUser command)
        {
            await _busClient.PublishAsync(command);
            return Accepted();
        }
    }
}