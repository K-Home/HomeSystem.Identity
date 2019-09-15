using System;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Domain.Aggregates;
using FinanceControl.Services.Users.Domain.SeedWork;

namespace FinanceControl.Services.Users.Domain.Repositories
{
    public interface IUserSessionRepository : IRepository<UserSession>
    {
        Task<UserSession> GetByIdAsync(Guid id);
        Task AddAsync(UserSession session);
        void Update(UserSession session);
        void Delete(UserSession session);
    }
}