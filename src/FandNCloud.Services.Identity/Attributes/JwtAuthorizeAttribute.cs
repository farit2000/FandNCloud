using System.Threading.Tasks;
using FandNCloud.Services.Identity.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;

namespace FandNCloud.Services.Identity.Attributes
{
    public class JwtAuthorizeAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var invalidTokenRepository = (IInvalidTokenRepository)context.HttpContext.RequestServices.GetService(typeof(IInvalidTokenRepository));
            var field = context.HttpContext.Request.Headers[HeaderNames.Authorization];
            if (field.Count == 0)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var accessToken = field.ToString().Replace("Bearer ", "");
            var item = await invalidTokenRepository.ContainsAsync(x => x.Token == accessToken);
            if (item)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}