using System.Threading.Tasks;
using HomeSystem.Services.Identity.Domain.Aggregates;

namespace HomeSystem.Services.Identity.DAL.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetAsync(string token);
        Task AddAsync(RefreshToken token);
        Task UpdateAsync(RefreshToken token);
    }
}