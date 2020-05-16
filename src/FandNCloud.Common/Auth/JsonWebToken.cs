namespace FandNCloud.Common.Auth
{
    public class JsonWebToken
    {
        public AccessToken AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }        
    }
}