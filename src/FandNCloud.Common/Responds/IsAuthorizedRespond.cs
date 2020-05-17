using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FandNCloud.Common.Responds
{
    public class IsAuthorizedRespond : IRespond
    {
        public int HttpCode{ get; set; }

        public IsAuthorizedRespond(int? httpCode)
        {
            if (httpCode == null)
                HttpCode = (int) HttpStatusCode.Unauthorized;
            else 
                HttpCode = (int)httpCode;
        }
    }
}