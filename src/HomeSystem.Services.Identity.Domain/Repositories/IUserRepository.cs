using HomeSystem.Services.Identity.Domain.Aggregates;
using HomeSystem.Services.Identity.Domain.SeedWork;
using System;
using System.Threading.Tasks;

namespace HomeSystem.Services.Identity.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> ExistsAsync(Guid userId);
        Task<User> GetByUserIdAsync(Guid userId);
        Task<User> GetByEmailAsync(string email);
        Task AddUserAsync(User user);
        void EditUser(User user);
        void DeleteUser(User user);
    }
}