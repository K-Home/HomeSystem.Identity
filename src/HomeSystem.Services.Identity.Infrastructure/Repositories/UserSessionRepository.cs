using System;
using System.Threading.Tasks;
using HomeSystem.Services.Identity.Domain.Aggregates;
using HomeSystem.Services.Identity.Domain.Repositories;
using HomeSystem.Services.Identity.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace HomeSystem.Services.Identity.Infrastructure.Repositories
{
    public class UserSessionRepository : IUserSessionRepository
    {
        private readonly IdentityDbContext _identityDbContext;

        public UserSessionRepository(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<UserSession> GetByIdAsync(Guid id)
            => await _identityDbContext.UserSessions.SingleOrDefaultAsync(us => us.Id == id);

        public async Task AddAsync(UserSession session)
        {
            await _identityDbContext.UserSessions.AddAsync(session);
            await _identityDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserSession session)
        {
            _identityDbContext.UserSessions.Update(session);
            await _identityDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var userSession = await GetByIdAsync(id);
            _identityDbContext.UserSessions.Remove(userSession);
            await _identityDbContext.SaveChangesAsync();
        }
    }
}