using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using FandNCloud.Common.Requests;
using FandNCloud.Common.Responds;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using RawRabbit;

namespace FandNCloud.Api.Attributes
{
    public class JwtAuthorizeAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var busClient = (IBusClient)context.HttpContext.RequestServices.GetService(typeof(IBusClient));
            var message = new IsAuthorizedRequest(context.HttpContext.Request.Headers[HeaderNames.Authorization]);
            var resultContext = await busClient.RequestAsync<IsAuthorizedRequest, IsAuthorizedRespond>(
                message);
            if (resultContext.HttpCode == (int)HttpStatusCode.Unauthorized)
                context.Result = new UnauthorizedResult();
        }
    }
}