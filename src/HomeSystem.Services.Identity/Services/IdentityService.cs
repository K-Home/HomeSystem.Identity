using System;
using System.Threading.Tasks;
using HomeSystem.Services.Identity.DAL.Repositories;
using HomeSystem.Services.Identity.Domain.Aggregates;
using HomeSystem.Services.Identity.Domain.Enumerations;
using HomeSystem.Services.Identity.Exceptions;
using KShared.Authentication.Services;
using KShared.Authentication.Tokens;
using KShared.Exceptions.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace HomeSystem.Services.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtHandler _jwtHandler;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IClaimsProvider _claimsProvider;

        public IdentityService(IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher,
            IJwtHandler jwtHandler,
            IRefreshTokenRepository refreshTokenRepository,
            IClaimsProvider claimsProvider)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtHandler = jwtHandler;
            _refreshTokenRepository = refreshTokenRepository;
            _claimsProvider = claimsProvider;
        }

        public async Task SignUpAsync(Guid id, string firstName, string lastName, string email, string password,
            string role = Role.User)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user != null)
            {
                throw new ServiceException(Codes.EmailInUse,
                    $"Email: '{email}' is already in use.");
            }

            if (string.IsNullOrWhiteSpace(role))
            {
                role = Role.User;
            }

            user = new User(id, firstName, lastName, email, role);
            user.SetPassword(password, _passwordHasher);
            await _userRepository.AddUserAsync(user);
        }

        public async Task<JsonWebToken> SignInAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null || !user.ValidatePassword(password, _passwordHasher))
            {
                throw new ServiceException(Codes.InvalidCredentials,
                    "Invalid credentials.");
            }

            var refreshToken = new RefreshToken(user, _passwordHasher);
            var claims = await _claimsProvider.GetAsync(user.AggregateId);
            var jwt = _jwtHandler.CreateToken(user.AggregateId.ToString("N"), user.Role, claims);
            jwt.RefreshToken = refreshToken.Token;
            await _refreshTokenRepository.AddAsync(refreshToken);

            return jwt;
        }

        public async Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetByUserIdAsync(userId);
            if (user == null)
            {
                throw new ServiceException(Codes.UserNotFound,
                    $"User with id: '{userId}' was not found.");
            }

            if (!user.ValidatePassword(currentPassword, _passwordHasher))
            {
                throw new ServiceException(Codes.InvalidCurrentPassword,
                    "Invalid current password.");
            }

            user.SetPassword(newPassword, _passwordHasher);
            await _userRepository.EditUserAsync(user);
        }
    }
}