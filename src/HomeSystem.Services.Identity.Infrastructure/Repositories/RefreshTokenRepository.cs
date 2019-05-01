using System.Threading.Tasks;
using HomeSystem.Services.Identity.Domain.Aggregates;
using HomeSystem.Services.Identity.Domain.Repositories;
using HomeSystem.Services.Identity.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace HomeSystem.Services.Identity.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IdentityDbContext _identityDbContext;

        public RefreshTokenRepository(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<RefreshToken> GetAsync(string token)
            => await _identityDbContext.RefreshTokens.SingleOrDefaultAsync(x => x.Token == token);

        public async Task AddAsync(RefreshToken token)
        {
            await _identityDbContext.AddAsync(token);
            await _identityDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(RefreshToken token)
        {
            _identityDbContext.Update(token);
            await _identityDbContext.SaveChangesAsync();
        }
    }
}