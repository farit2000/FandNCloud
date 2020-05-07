using System.Threading.Tasks;

namespace FandNCloud.Services.Identity.Services
{
    public interface IUserService
    {
        Task RegisterAsync(string email, string password, string name);
        // Task<JsonWebToken> LoginAsync(string email, string password);
    }
}