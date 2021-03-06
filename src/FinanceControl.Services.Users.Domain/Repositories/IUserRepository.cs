using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Domain.Aggregates;
using FinanceControl.Services.Users.Domain.SeedWork;

namespace FinanceControl.Services.Users.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> ExistsAsync(Expression<Func<User, bool>> predicate);
        Task<User> GetByUserIdAsync(Guid userId);
        Task<User> GetByNameAsync(string name);
        Task<User> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetUsers();
        Task<string> GetStateAsync(Guid id);
        Task AddUserAsync(User user);
        void EditUser(User user);
        void DeleteUser(User user);
    }
}