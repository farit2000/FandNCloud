using System.Threading.Tasks;
using FandNCloud.Common.Repositories;
using FandNCloud.Services.Identity.Domain.Models;

namespace FandNCloud.Services.Identity.Domain.Repositories
{
    public interface IInvalidTokenRepository : IRepository<InvalidToken, int>
    {
        // Task<InvalidToken> GetByValue(string accessToken);
    }
}