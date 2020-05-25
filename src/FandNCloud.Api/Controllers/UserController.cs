using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FandNCloud.Common.Commands;
using FandNCloud.Common.Requests;
using FandNCloud.Common.Responds;
using FandNCloud.Services.Identity.Attributes;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace FandNCloud.Api.Controllers
{
    // [ValidateActionParameters]
    [Route("[controller]")]
    [EnableCors]
    public class UserController : Controller
    {
        private readonly IBusClient _busClient;
        
        public UserController(IBusClient busClient)
        {
            _busClient = busClient;
        }

        [HttpPost("register")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] CreateUserRequest request)
        {
            var respond = await _busClient.RequestAsync<CreateUserRequest, CreateUserRespond>(request);
            return respond.Respond;
        }

        [HttpPost("login")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Login([FromForm] LoginUserRequest request)
        {
            var respond = await _busClient.RequestAsync<LoginUserRequest, LoginUserRespond>(request);
            return Json(respond.Result);
        }

        [HttpPost("logout")]
        [Consumes("application/x-www-form-urlencoded")]
        public async void Logout([FromHeader(Name = "Authorization")] [Required]
            string accessToken, [Required] string refreshToken)
        {
            await _busClient.PublishAsync(new LogoutUserCommand(accessToken, refreshToken));
        }

        [HttpPost("refresh")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> Refresh([FromHeader(Name = "Authorization")] [Required]
            string accessToken,
            [Required] string refreshToken)
        {
            var respond = await _busClient.RequestAsync<RefreshUserRequest,
                RefreshUserRespond>(new RefreshUserRequest(accessToken, refreshToken));
            return Json(respond.Result);
        }
    }
}