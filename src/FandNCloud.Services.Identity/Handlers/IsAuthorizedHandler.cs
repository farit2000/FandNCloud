using System.Threading.Tasks;
using FandNCloud.Common.Requests;
using FandNCloud.Common.Responds;
using FandNCloud.Services.Identity.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace FandNCloud.Services.Identity.Handlers
{
    public class IsAuthorizedHandler : IRequestHandler<IsAuthorizedRequest>
    {
        private IInvalidTokenRepository _invalidTokenRepository;
        
        public IsAuthorizedHandler(IInvalidTokenRepository invalidTokenRepository)
        {
            _invalidTokenRepository = invalidTokenRepository;
        }
        public async Task<IRespond> HandleAsync(IsAuthorizedRequest request)
        {
            var field = request.AuthorizationField;
            if (field.Length == 0)
            {
                return new IsAuthorizedRespond(new UnauthorizedResult().StatusCode);
            }
            var accessToken = field.ToString().Replace("Bearer ", "");
            var item = await _invalidTokenRepository.ContainsAsync(x => x.Token == accessToken);
            if (item)
            {
                return new IsAuthorizedRespond(new UnauthorizedResult().StatusCode);
            }
            return new IsAuthorizedRespond(new AcceptedResult().StatusCode);
        }
    }
}