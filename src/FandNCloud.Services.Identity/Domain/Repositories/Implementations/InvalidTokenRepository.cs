using System;
using FandNCloud.Services.Identity.Domain.Database;
using FandNCloud.Services.Identity.Domain.Models;
using FandNCloud.Services.Identity.Domain.Repositories;

namespace FandNCloud.Services.Identity.Repositories
{
    public class InvalidTokenRepository : Repository<InvalidToken, int>, IInvalidTokenRepository
    {
        public InvalidTokenRepository(Func<IdentityContext> contextFactory) : base(contextFactory)
        {
        }
    }
}