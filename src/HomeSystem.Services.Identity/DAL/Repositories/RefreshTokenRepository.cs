using System.Threading.Tasks;
using HomeSystem.Services.Identity.Domain.Aggregates;

namespace HomeSystem.Services.Identity.DAL.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IdentityDbContext _identityDbContext;

        public RefreshTokenRepository(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<RefreshToken> GetAsync(string token)
            => await _identityDbContext.RefreshTokens.FindAsync(token);

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