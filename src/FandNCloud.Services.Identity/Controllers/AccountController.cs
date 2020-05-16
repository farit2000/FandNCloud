using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using FandNCloud.Common.Commands;
using FandNCloud.Services.Identity.Attributes;
using FandNCloud.Services.Identity.Requests;
using FandNCloud.Services.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace FandNCloud.Services.Identity.Controllers
{
    [Route("")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        
        // [Unauthorized]
        // [HttpPost("register")]
        // public async void Register([FromForm] CreateUser command)
        //     => await _userService.RegisterAsync(command.Email, command.Password, command.FirstName, command.LastName);
        
        [Unauthorized]
        [ValidateActionParameters]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginRequest request)
            => Json(await _userService.LoginAsync(request.Email, request.Password));
        
        // [JwtAuthorize]
        [ValidateActionParameters]
        [HttpPost("logout")]
        [Consumes("application/x-www-form-urlencoded")]
        public async void Logout([FromHeader(Name="Authorization")] [Required] string accessToken, [Required] string refreshToken)
            => await _userService.LogoutAsync(accessToken.Replace("Bearer ", ""), refreshToken);
        
        
        [HttpPost("refresh")]
        [ValidateActionParameters]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> Refresh([FromHeader(Name="Authorization")] [Required] string accessToken, [Required] string refreshToken)
        {
            return Json(await _userService.RefreshAsync(accessToken.Replace("Bearer ", ""), refreshToken));
        }
    }
}