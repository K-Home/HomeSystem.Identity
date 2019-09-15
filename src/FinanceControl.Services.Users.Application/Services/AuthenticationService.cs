using System;
using System.Threading;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Application.Exceptions;
using FinanceControl.Services.Users.Application.Services.Base;
using FinanceControl.Services.Users.Domain;
using FinanceControl.Services.Users.Domain.Aggregates;
using FinanceControl.Services.Users.Domain.Enumerations;
using FinanceControl.Services.Users.Domain.Repositories;
using FinanceControl.Services.Users.Domain.Services;

namespace FinanceControl.Services.Users.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserSessionRepository _userSessionRepository;
        private readonly IEncrypter _encrypter;

        public AuthenticationService(IUserRepository userRepository,
            IUserSessionRepository userSessionRepository,
            IEncrypter encrypter)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _userSessionRepository =
                userSessionRepository ?? throw new ArgumentNullException(nameof(userSessionRepository));
            _encrypter = encrypter ?? throw new ArgumentNullException(nameof(encrypter));
        }

        public async Task<UserSession> GetSessionAsync(Guid id)
        {
            return await _userSessionRepository.GetByIdAsync(id);
        }

        public async Task SignInAsync(Guid sessionId, string email, string password,
            string ipAddress = null, string userAgent = null)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
                throw new ServiceException(Codes.UserNotFound,
                    $"User with email '{email}' has not been found.");

            if (user.State != States.Active && user.State != States.Unconfirmed)
                throw new ServiceException(Codes.InactiveUser,
                    $"User '{user.Id}' is not active.");

            if (!user.ValidatePassword(password, _encrypter))
                throw new ServiceException(Codes.CredentialsAreInvalid,
                    "Invalid credentials.");

            await CreateSessionAsync(sessionId, user);
        }

        public async Task SignOutAsync(Guid sessionId, Guid userId)
        {
            var user = await _userRepository.GetByUserIdAsync(userId);

            if (user == null)
                throw new ServiceException(Codes.UserNotFound,
                    $"User with id '{userId}' has not been found.");

            var session = await _userSessionRepository.GetByIdAsync(sessionId);

            if (session == null)
                throw new ServiceException(Codes.SessionNotFound,
                    $"Session with id '{sessionId}' has not been found.");

            session.Destroy();
            _userSessionRepository.Update(session);
        }

        public async Task CreateSessionAsync(Guid sessionId, Guid userId,
            string ipAddress = null,
            string userAgent = null)
        {
            var user = await _userRepository.GetByUserIdAsync(userId);

            if (user == null)
                throw new ServiceException(Codes.UserNotFound,
                    $"User with id '{userId}' has not been found.");

            await CreateSessionAsync(sessionId, user);
        }

        private async Task CreateSessionAsync(Guid sessionId, User user,
            string ipAddress = null, string userAgent = null)
        {
            var session = new UserSession(sessionId, user.Id,
                _encrypter.GetRandomSecureKey(), ipAddress, userAgent);

            await _userSessionRepository.AddAsync(session);
        }

        public async Task RefreshSessionAsync(Guid sessionId, Guid newSessionId,
            string sessionKey, string ipAddress = null, string userAgent = null)
        {
            var parentSession = await _userSessionRepository.GetByIdAsync(sessionId);

            if (parentSession == null)
                throw new ServiceException(Codes.SessionNotFound,
                    $"Session with id '{sessionId}' has not been found.");

            if (parentSession.Key != sessionKey)
                throw new ServiceException(Codes.SessionKeyIsInvalid,
                    $"Invalid session key: '{sessionKey}'");

            var newSession = parentSession.Refresh(newSessionId,
                _encrypter.GetRandomSecureKey(), sessionId, ipAddress, userAgent);

            await _userSessionRepository.AddAsync(newSession);
            _userSessionRepository.Delete(parentSession);
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _userSessionRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}