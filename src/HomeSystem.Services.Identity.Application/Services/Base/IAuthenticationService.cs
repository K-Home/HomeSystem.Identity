using System;
using System.Threading.Tasks;
using HomeSystem.Services.Identity.Domain.Aggregates;

namespace HomeSystem.Services.Identity.Application.Services.Base
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
    }
}