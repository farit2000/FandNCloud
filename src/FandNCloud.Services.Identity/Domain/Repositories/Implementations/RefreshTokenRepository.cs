using System;
using System.Threading.Tasks;
using FandNCloud.Services.Identity.Domain.Database;
using FandNCloud.Services.Identity.Domain.Models;
using FandNCloud.Services.Identity.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FandNCloud.Services.Identity.Repositories
{
    public class RefreshTokenRepository : Repository<RefreshToken, int>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(Func<IdentityContext> contextFactory) : base(contextFactory)
        {
        }
        
        public async Task<RefreshToken> GetByUserIdAndToken(Guid userId, string refreshToken)
        {
            using (var context = ContextFactory())
            {
                return await context.RefreshTokens
                    // .Include(e => e.User)
                    .FirstOrDefaultAsync(e=> e.UserId == userId && e.Token == refreshToken);
            }
        }
    }
}