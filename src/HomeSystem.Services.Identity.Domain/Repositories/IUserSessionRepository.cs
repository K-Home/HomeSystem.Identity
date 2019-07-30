using HomeSystem.Services.Identity.Domain.Aggregates;
using HomeSystem.Services.Identity.Domain.SeedWork;
using System;
using System.Threading.Tasks;

namespace HomeSystem.Services.Identity.Domain.Repositories
{
    public interface IUserSessionRepository : IRepository<UserSession>
    {
        Task<UserSession> GetByIdAsync(Guid id);
        Task AddAsync(UserSession session);
        void Update(UserSession session);
        void Delete(UserSession session);
    }
}