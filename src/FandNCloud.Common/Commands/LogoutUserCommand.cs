namespace FandNCloud.Common.Commands
{
    public class LogoutUserCommand : ICommand
    {
        public string AccessToken { get; set; }
        
        public string RefreshToken { get; set; }

        public LogoutUserCommand(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}