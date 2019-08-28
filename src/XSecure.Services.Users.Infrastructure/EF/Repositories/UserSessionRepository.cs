using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using XSecure.Services.Users.Domain.Aggregates;
using XSecure.Services.Users.Domain.Repositories;
using XSecure.Services.Users.Domain.SeedWork;

namespace XSecure.Services.Users.Infrastructure.EF.Repositories
{
    public class UserSessionRepository : IUserSessionRepository
    {
        private readonly IdentityDbContext _identityDbContext;

        public IUnitOfWork UnitOfWork => _identityDbContext;

        public UserSessionRepository(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<UserSession> GetByIdAsync(Guid id)
            => await _identityDbContext.UserSessions.SingleOrDefaultAsync(us => us.Id == id);

        public async Task AddAsync(UserSession session)
        {
            await _identityDbContext.UserSessions.AddAsync(session);
        }

        public void Update(UserSession session)
        {
            _identityDbContext.Entry(session).State = EntityState.Modified;
        }

        public void Delete(UserSession session)
        {
            _identityDbContext.UserSessions.Remove(session);
        }
    }
}