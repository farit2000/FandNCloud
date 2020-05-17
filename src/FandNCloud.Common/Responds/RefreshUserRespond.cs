using Microsoft.AspNetCore.Mvc;
using FandNCloud.Common.Auth;

namespace FandNCloud.Common.Responds
{
    public class RefreshUserRespond : IRespond
    {
        public JsonWebToken Result;

        public RefreshUserRespond(JsonWebToken result)
        {
            Result = result;
        }
    }
}