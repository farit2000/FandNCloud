using Microsoft.AspNetCore.Mvc;
using FandNCloud.Common.Auth;

namespace FandNCloud.Common.Responds
{
    public class LoginUserRespond : IRespond
    {
        public JsonWebToken Result { get; set; }

        public LoginUserRespond(JsonWebToken result)
        {
            Result = result;
        }
    }
}