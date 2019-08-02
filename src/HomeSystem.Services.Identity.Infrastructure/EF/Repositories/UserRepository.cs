using HomeSystem.Services.Identity.Domain.Aggregates;
using HomeSystem.Services.Identity.Domain.Repositories;
using HomeSystem.Services.Identity.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace HomeSystem.Services.Identity.Infrastructure.EF.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityDbContext _identityDbContext;

        public IUnitOfWork UnitOfWork => _identityDbContext;

        public UserRepository(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<bool> ExistsAsync(Guid userId)
            => await _identityDbContext.Users.AnyAsync(x => x.Id == userId);

        public async Task<User> GetByUserIdAsync(Guid userId)
            => await _identityDbContext.Users.SingleOrDefaultAsync(x => x.Id == userId);

        public async Task<User> GetByEmailAsync(string email)
            => await _identityDbContext.Users.SingleOrDefaultAsync(x => x.Email == email);

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