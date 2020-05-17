using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FandNCloud.Common.Requests
{
    public class IsAuthorizedRequest : IRequest
    {
        public string AuthorizationField { get; set; }

        public IsAuthorizedRequest(string authorizationField)
        {
            AuthorizationField = authorizationField;
        }
    }
}