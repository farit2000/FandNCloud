namespace FandNCloud.Common.Auth
{
    public interface IRefreshTokenFactory
    {
        string GenerateRefreshToken(int size=32);
    }
}