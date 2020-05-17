using System;
using System.Threading.Tasks;
using FandNCloud.Common.Repositories;
using FandNCloud.Services.Identity.Domain.Models;

namespace FandNCloud.Services.Identity.Domain.Repositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        Task<User> GetAsync(string email);
    }
}