namespace FandNCloud.Services.Identity.Domain
{
    public interface IEncrypter
    {
        string GetSalt();
        string GetHash(string value, string salt);
    }
}