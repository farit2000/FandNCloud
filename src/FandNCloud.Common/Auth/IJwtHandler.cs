using System;

namespace FandNCloud.Common.Auth
{
    public interface IJwtHandler
    {
        JsonWebToken Create(Guid userId);
        Guid RetrieveUserIdFromAccessToken(string accessToken);
    }
}