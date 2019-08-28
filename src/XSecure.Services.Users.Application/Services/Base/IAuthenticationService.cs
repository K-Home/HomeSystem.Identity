using System;
using System.Threading;
using System.Threading.Tasks;
using XSecure.Services.Users.Domain.Aggregates;

namespace XSecure.Services.Users.Application.Services.Base
{
    public interface IAuthenticationService
    {
        Task<UserSession> GetSessionAsync(Guid id);

        Task SignInAsync(Guid sessionId, string email, string password,
            string ipAddress = null, string userAgent = null);

        Task SignOutAsync(Guid sessionId, Guid userId);

        Task CreateSessionAsync(Guid sessionId, Guid userId,
            string ipAddress = null, string userAgent = null);

        Task RefreshSessionAsync(Guid sessionId, Guid newSessionId,
            string sessionKey, string ipAddress = null, string userAgent = null);

        Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}