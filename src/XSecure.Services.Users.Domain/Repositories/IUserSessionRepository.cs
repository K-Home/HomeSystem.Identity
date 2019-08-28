using System;
using System.Threading.Tasks;
using XSecure.Services.Users.Domain.Aggregates;
using XSecure.Services.Users.Domain.SeedWork;

namespace XSecure.Services.Users.Domain.Repositories
{
    public interface IUserSessionRepository : IRepository<UserSession>
    {
        Task<UserSession> GetByIdAsync(Guid id);
        Task AddAsync(UserSession session);
        void Update(UserSession session);
        void Delete(UserSession session);
    }
}