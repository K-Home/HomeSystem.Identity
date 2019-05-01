using HomeSystem.Services.Identity.Domain.Aggregates;
using System.Threading.Tasks;

namespace HomeSystem.Services.Identity.Domain.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetAsync(string token);
        Task AddAsync(RefreshToken token);
        Task UpdateAsync(RefreshToken token);
    }
}