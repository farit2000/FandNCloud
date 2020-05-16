using System;
using System.Threading.Tasks;
using FandNCloud.Common.Repositories;
using FandNCloud.Services.Identity.Domain.Models;

namespace FandNCloud.Services.Identity.Domain.Repositories
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken, int>
    {
        Task<RefreshToken> GetByUserIdAndToken(Guid userId, string refreshToken);
    }
}