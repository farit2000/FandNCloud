using System.Threading.Tasks;
using FandNCloud.Common.Commands;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace FandNCloud.Api.Controllers
{
    [Route("")]
    [EnableCors]
    public class UserController : Controller
    {
        private readonly IBusClient _busClient;
        
        public UserController(IBusClient busClient)
        {
            _busClient = busClient;
        }

        [HttpPost("register")]
        // [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] CreateUser command)
        {
            await _busClient.PublishAsync(command);
            return Accepted();
        }
    }
}