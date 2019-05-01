using System;
using System.Threading.Tasks;
using HomeSystem.Services.Identity.Domain.Aggregates;
using HomeSystem.Services.Identity.Domain.Repositories;
using HomeSystem.Services.Identity.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace HomeSystem.Services.Identity.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityDbContext _identityDbContext;

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
            await _identityDbContext.SaveChangesAsync();
        }

        public async Task EditUserAsync(User user)
        {
            _identityDbContext.Users.Update(user);
            await _identityDbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            var user = await GetByUserIdAsync(userId);
            _identityDbContext.Users.Remove(user);
            await _identityDbContext.SaveChangesAsync();
        }
    }
}