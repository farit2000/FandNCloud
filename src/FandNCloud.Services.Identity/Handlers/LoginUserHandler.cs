using System.Threading.Tasks;
using FandNCloud.Common.Requests;
using FandNCloud.Common.Responds;
using FandNCloud.Services.Identity.Services;

namespace FandNCloud.Services.Identity.Handlers
{
    public class LoginUserHandler : IRequestHandler<LoginUserRequest>
    {
        private readonly IUserService _userService;
        
        public LoginUserHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IRespond> HandleAsync(LoginUserRequest request)
        {
            var token = await _userService.LoginAsync(request.Email, request.Password);
            return new LoginUserRespond(token);
        }
    }
}