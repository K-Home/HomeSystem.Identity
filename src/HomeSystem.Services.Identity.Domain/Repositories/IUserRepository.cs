using HomeSystem.Services.Identity.Domain.Aggregates;
using HomeSystem.Services.Identity.Domain.SeedWork;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HomeSystem.Services.Identity.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> ExistsAsync(Expression<Func<User, bool>> predicate);
        Task<User> GetByUserIdAsync(Guid userId);
        Task<User> GetByNameAsync(string name);
        Task<User> GetByEmailAsync(string email);
        Task<string> GetStateAsync(Guid id);
        Task AddUserAsync(User user);
        void EditUser(User user);
        void DeleteUser(User user);
    }
}