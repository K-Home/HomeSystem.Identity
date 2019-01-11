using System;
using System.Threading.Tasks;
using HomeSystem.Services.Identity.DAL.Repositories;
using HomeSystem.Services.Identity.Domain.Aggregates;
using HomeSystem.Services.Identity.Exceptions;
using KShared.Authentication.Services;
using KShared.Authentication.Tokens;
using KShared.Exceptions.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace HomeSystem.Services.Identity.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJwtHandler _jwtHandler;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IClaimsProvider _claimsProvider;

        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository,
            IUserRepository userRepository,
            IJwtHandler jwtHandler,
            IPasswordHasher<User> passwordHasher,
            IClaimsProvider claimsProvider)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
            _passwordHasher = passwordHasher;
            _claimsProvider = claimsProvider;
        }

        public async Task AddAsync(Guid userId)
        {
            var user = await _userRepository.GetByUserIdAsync(userId);
            if (user == null)
            {
                throw new ServiceException(Codes.UserNotFound,
                    $"User: '{userId}' was not found.");
            }

            await _refreshTokenRepository.AddAsync(new RefreshToken(user, _passwordHasher));
        }

        public async Task<JsonWebToken> CreateAccessTokenAsync(string token)
        {
            var refreshToken = await _refreshTokenRepository.GetAsync(token);
            if (refreshToken == null)
            {
                throw new ServiceException(Codes.RefreshTokenNotFound,
                    "Refresh token was not found.");
            }

            if (refreshToken.Revoked)
            {
                throw new ServiceException(Codes.RefreshTokenAlreadyRevoked,
                    $"Refresh token: '{refreshToken.Id}' was revoked.");
            }

            var user = await _userRepository.GetByUserIdAsync(refreshToken.UserId);
            if (user == null)
            {
                throw new ServiceException(Codes.UserNotFound,
                    $"User: '{refreshToken.UserId}' was not found.");
            }

            var claims = await _claimsProvider.GetAsync(user.AggregateId);
            var jwt = _jwtHandler.CreateToken(user.AggregateId.ToString("N"), user.Role, claims);
            jwt.RefreshToken = refreshToken.Token;

            return jwt;
        }

        public async Task RevokeAsync(string token, Guid userId)
        {
            var refreshToken = await _refreshTokenRepository.GetAsync(token);
            if (refreshToken == null || refreshToken.UserId != userId)
            {
                throw new ServiceException(Codes.RefreshTokenNotFound,
                    "Refresh token was not found.");
            }

            refreshToken.Revoke();
            await _refreshTokenRepository.UpdateAsync(refreshToken);
        }
    }
}