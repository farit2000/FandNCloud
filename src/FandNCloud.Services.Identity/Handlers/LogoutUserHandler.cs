using System.Threading.Tasks;
using FandNCloud.Common.Commands;
using FandNCloud.Services.Identity.Services;

namespace FandNCloud.Services.Identity.Handlers
{
    public class LogoutUserHandler : ICommandHandler<LogoutUserCommand>
    {
        private readonly IUserService _userService;
        
        public LogoutUserHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task HandleAsync(LogoutUserCommand command)
        {
            await _userService.LogoutAsync(command.AccessToken.Replace("Bearer ", ""),
                command.RefreshToken);
        }
    }
}