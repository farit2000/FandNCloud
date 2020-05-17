namespace FandNCloud.Common.Requests
{
    public class RefreshUserRequest : IRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public RefreshUserRequest(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}