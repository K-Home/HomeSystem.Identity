using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using XSecure.Services.Users.Domain.Aggregates;
using XSecure.Services.Users.Domain.Repositories;
using XSecure.Services.Users.Domain.SeedWork;

namespace XSecure.Services.Users.Infrastructure.EF.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityDbContext _identityDbContext;

        public IUnitOfWork UnitOfWork => _identityDbContext;

        public UserRepository(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<bool> ExistsAsync(Expression<Func<User, bool>> predicate)
            => await _identityDbContext.Users.AnyAsync(predicate);

        public async Task<User> GetByUserIdAsync(Guid userId)
            => await _identityDbContext.Users.SingleOrDefaultAsync(x => x.Id == userId);

        public async Task<User> GetByNameAsync(string name)
            => await _identityDbContext.Users.SingleOrDefaultAsync(x => x.Username == name);

        public async Task<User> GetByEmailAsync(string email)
            => await _identityDbContext.Users.SingleOrDefaultAsync(x => x.Email == email);

        public async Task<IEnumerable<User>> GetUsers()
            => await _identityDbContext.Users.ToListAsync();
        
        public async Task<string> GetStateAsync(Guid id)
        {
            var user = await _identityDbContext.Users.SingleOrDefaultAsync(x => x.Id == id);
            var userState = user.State;

            return userState;
        }

        public async Task AddUserAsync(User user)
        {
            await _identityDbContext.Users.AddAsync(user);
        }

        public void EditUser(User user)
        {
            _identityDbContext.Entry(user).State = EntityState.Modified;
        }

        public void DeleteUser(User user)
        {
            _identityDbContext.Users.Remove(user);
        }
    }
}