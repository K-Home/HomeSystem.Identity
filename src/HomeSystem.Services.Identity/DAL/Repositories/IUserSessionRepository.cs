using System;
using System.Threading.Tasks;
using HomeSystem.Services.Identity.Domain.Aggregates;

namespace HomeSystem.Services.Identity.DAL.Repositories
{
    public interface IUserSessionRepository
    {
        Task<UserSession> GetByIdAsync(Guid id);
        Task AddAsync(UserSession session);
        Task UpdateAsync(UserSession session);
        Task DeleteAsync(Guid id);
    }
}