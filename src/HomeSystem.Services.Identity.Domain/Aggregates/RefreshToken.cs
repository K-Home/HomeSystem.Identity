using HomeSystem.Services.Identity.Domain.Exceptions;
using HomeSystem.Services.Identity.Domain.Types;
using HomeSystem.Services.Identity.Domain.Types.Base;
using HomeSystem.Services.Identity.Exceptions;
using Microsoft.AspNetCore.Identity;
using System;

namespace HomeSystem.Services.Identity.Domain.Aggregates
{
    public class RefreshToken : EntityBase, ITimestampable
    {
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public string Token { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? RevokedAt { get; private set; }
        public bool Revoked { get; private set; }

        protected RefreshToken()
        {
        }

        public RefreshToken(User user, IPasswordHasher<User> passwordHasher)
        {
            Id = Guid.NewGuid();
            User = user;
            UserId = user.Id;
            CreatedAt = DateTime.UtcNow;
            Token = CreateToken(user, passwordHasher);
        }

        public void Revoke()
        {
            if (Revoked)
            {
                throw new DomainException(Codes.RefreshTokenAlreadyRevoked,
                    $"Refresh token: '{Id}' was already revoked at '{RevokedAt}'.");
            }
            RevokedAt = DateTime.UtcNow;
            Revoked = true;
        }

        private static string CreateToken(User user, IPasswordHasher<User> passwordHasher)
            => passwordHasher.HashPassword(user, Guid.NewGuid().ToString("N"))
                .Replace("=", string.Empty)
                .Replace("+", string.Empty)
                .Replace("/", string.Empty);
    }
}