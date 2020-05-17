using System;
using System.Threading.Tasks;
using FandNCloud.Services.Identity.Domain.Database;
using FandNCloud.Services.Identity.Domain.Models;
using FandNCloud.Services.Identity.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FandNCloud.Services.Identity.Repositories
{
    public class UserRepository : Repository<User, Guid>, IUserRepository
    {
        public UserRepository(Func<IdentityContext> contextFactory) : base(contextFactory)
        {
        }
        public async Task<User> GetAsync(string email)
        {            
            using (var context = ContextFactory())
            {
                return await context.Users.FirstOrDefaultAsync(e=> e.Email == email);
            }
        }
    }
}