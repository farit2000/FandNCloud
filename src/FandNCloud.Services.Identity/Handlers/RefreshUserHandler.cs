using System.Threading.Tasks;
using FandNCloud.Common.Requests;
using FandNCloud.Common.Responds;
using FandNCloud.Services.Identity.Services;

namespace FandNCloud.Services.Identity.Handlers
{
    public class RefreshUserHandler : IRequestHandler<RefreshUserRequest>
    {
        private readonly IUserService _userService;
        
        public RefreshUserHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IRespond> HandleAsync(RefreshUserRequest request)
        {
            var token = await _userService
                .RefreshAsync(request.AccessToken.Replace("Bearer ", ""), request.RefreshToken);
            return new RefreshUserRespond(token);
        }
    }
}