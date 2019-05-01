using HomeSystem.Services.Identity.Domain.Aggregates;
using System;
using System.Threading.Tasks;

namespace HomeSystem.Services.Identity.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<bool> ExistsAsync(Guid userId);
        Task<User> GetByUserIdAsync(Guid userId);
        Task<User> GetByEmailAsync(string email);
        Task AddUserAsync(User user);
        Task EditUserAsync(User user);
        Task DeleteUserAsync(Guid userId);
    }
}