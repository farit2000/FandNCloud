using System.Threading.Tasks;
using FandNCloud.Common.Auth;
using FandNCloud.Services.Identity.Domain.Models;

namespace FandNCloud.Services.Identity.Services
{
    public interface IUserService
    {
        Task<User> RegisterAsync(string email, string password,  string firstName, string lastName);
        Task<JsonWebToken> LoginAsync(string email, string password);
        Task LogoutAsync(string accessToken, string refreshToken);
        Task<JsonWebToken> RefreshAsync(string accessToken, string refreshToken);
    }
}