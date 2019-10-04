using System;
using System.Threading;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Application.Exceptions;
using FinanceControl.Services.Users.Application.Services.Base;
using FinanceControl.Services.Users.Domain;
using FinanceControl.Services.Users.Domain.Aggregates;
using FinanceControl.Services.Users.Domain.Enumerations;
using FinanceControl.Services.Users.Domain.Extensions;
using FinanceControl.Services.Users.Domain.Repositories;
using FinanceControl.Services.Users.Domain.Services;
using FinanceControl.Services.Users.Infrastructure.Authorization;
using Microsoft.Extensions.Logging;

namespace FinanceControl.Services.Users.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IUserSessionRepository _userSessionRepository;
        private readonly IJwtTokenHandler _jwtTokenHandler;
        private readonly IEncrypter _encrypter;

        public AuthenticationService(ILogger<AuthenticationService> logger,
            IUserRepository userRepository, IUserSessionRepository userSessionRepository,
            IJwtTokenHandler jwtTokenHandler, IEncrypter encrypter)
        {
            _logger = logger.CheckIfNotEmpty();
            _userRepository = userRepository.CheckIfNotEmpty();
            _userSessionRepository = userSessionRepository.CheckIfNotEmpty();
            _encrypter = encrypter.CheckIfNotEmpty();
            _jwtTokenHandler = jwtTokenHandler.CheckIfNotEmpty();
        }

        public async Task<UserSession> GetSessionAsync(Guid id)
        {
            return await _userSessionRepository.GetByIdAsync(id);
        }

        public async Task<string> GetTokenFromHeader(string authorizationHeader)
        {
            var token = _jwtTokenHandler.GetFromAuthorizationHeader(authorizationHeader);

            if (token.IsEmpty())
            {
                _logger.LogError($"Can not get token from header, because value from authorization header is empty.");
            }

            return await Task.FromResult(token);
        }

        public async Task CheckIpAddressOrDevice(string ipAddress, string userAgent, string token)
        {
            var jwtValues = _jwtTokenHandler.Parse(token);
            if (jwtValues.HasNoValue())
            {
                _logger.LogError($"Unable to get jwt details, because token is empty or invalid.");
                return;
            }

            var success = Guid.TryParse(jwtValues.SessionId, out var sessionId);
            if (!success)
            {
                _logger.LogError($"Parsing sessionId failed. SessionId: {sessionId}");
                return;
            }

            var session = await _userSessionRepository.GetByIdAsync(sessionId);
            if (session.HasNoValue())
            {
                _logger.LogError($"Session with id: {sessionId} was not found!");
                return;
            }

            if (jwtValues.IpAddress != ipAddress || jwtValues.UserAgent != userAgent)
            {
                _logger.LogWarning($"Destroying session with id: {sessionId} for user with id: {session.UserId}" +
                                   ", because detect attempt to login from another location.");

                session.Destroy();
                _userSessionRepository.Update(session);
                await _userSessionRepository.UnitOfWork.SaveEntitiesAsync();
            }
        }

        public async Task<JwtSession> GetJwtSessionAsync(Guid sessionId, string ipAddress, string userAgent)
        {
            var session = await _userSessionRepository.GetByIdAsync(sessionId);
            if (session.HasNoValue())
            {
                return null;
            }

            var user = await _userRepository.GetByUserIdAsync(session.UserId);
            var token = _jwtTokenHandler.Create(user.Id, session.Id,
                user.Role, user.State, ipAddress, userAgent);

            return new JwtSession
            {
                Token = token.Token,
                Expires = token.Expires,
                SessionId = session.Id,
                Key = session.Key
            };
        }

        public async Task SignInAsync(Guid sessionId, string email, string password,
            string ipAddress, string userAgent)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user.HasNoValue())
            {
                throw new ServiceException(Codes.UserNotFound,
                    $"User with email '{email}' has not been found.");
            }

            if (user.State != States.Active && user.State != States.Unconfirmed)
            {
                throw new ServiceException(Codes.InactiveUser,
                    $"User '{user.Id}' is not active.");
            }

            if (!user.ValidatePassword(password, _encrypter))
            {
                throw new ServiceException(Codes.CredentialsAreInvalid,
                    "Invalid credentials.");
            }

            await CreateSessionAsync(sessionId, user, ipAddress, userAgent);
        }

        public async Task SignOutAsync(Guid sessionId, Guid userId)
        {
            var user = await _userRepository.GetByUserIdAsync(userId);

            if (user.HasNoValue())
            {
                throw new ServiceException(Codes.UserNotFound,
                    $"User with id '{userId}' has not been found.");
            }

            var session = await _userSessionRepository.GetByIdAsync(sessionId);

            if (session.HasNoValue())
            {
                throw new ServiceException(Codes.SessionNotFound,
                    $"Session with id '{sessionId}' has not been found.");
            }

            session.Destroy();
            _userSessionRepository.Update(session);
        }

        public async Task CreateSessionAsync(Guid sessionId, Guid userId,
            string ipAddress, string userAgent)
        {
            var user = await _userRepository.GetByUserIdAsync(userId);

            if (user.HasNoValue())
            {
                throw new ServiceException(Codes.UserNotFound,
                    $"User with id '{userId}' has not been found.");
            }

            await CreateSessionAsync(sessionId, user, ipAddress, userAgent);
        }

        private async Task CreateSessionAsync(Guid sessionId, User user,
            string ipAddress, string userAgent)
        {
            var session = new UserSession(sessionId, user.Id,
                _encrypter.GetRandomSecureKey(), ipAddress, userAgent);

            await _userSessionRepository.AddAsync(session);
        }

        public async Task RefreshSessionAsync(Guid sessionId, Guid newSessionId,
            string sessionKey, string ipAddress, string userAgent)
        {
            var parentSession = await _userSessionRepository.GetByIdAsync(sessionId);

            if (parentSession.HasNoValue())
            {
                throw new ServiceException(Codes.SessionNotFound,
                    $"Session with id '{sessionId}' has not been found.");
            }

            if (parentSession.Key != sessionKey)
            {
                throw new ServiceException(Codes.SessionKeyIsInvalid,
                    $"Invalid session key: '{sessionKey}'");
            }

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