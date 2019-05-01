using HomeSystem.Services.Identity.Domain.Aggregates;
using System;
using System.Threading.Tasks;

namespace HomeSystem.Services.Identity.Domain.Repositories
{
    public interface IUserSessionRepository
    {
        Task<UserSession> GetByIdAsync(Guid id);
        Task AddAsync(UserSession session);
        Task UpdateAsync(UserSession session);
        Task DeleteAsync(Guid id);
    }
}