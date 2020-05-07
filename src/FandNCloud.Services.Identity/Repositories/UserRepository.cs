using System;
using System.Threading.Tasks;
using FandNCloud.Services.Identity.Domain.Models;

namespace FandNCloud.Services.Identity.Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<User> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}