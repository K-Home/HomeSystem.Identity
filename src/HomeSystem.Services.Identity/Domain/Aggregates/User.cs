using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using HomeSystem.Services.Identity.Domain.Enumerations;
using HomeSystem.Services.Identity.Exceptions;
using KShared.Domain.BaseClasses;
using KShared.Exceptions.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace HomeSystem.Services.Identity.Domain.Aggregates
{
    public class User : AggregateRoot, IEditable, ITimestampable
    {
        private static readonly Regex EmailRegex = new Regex(
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

        private ISet<RefreshToken> _refreshTokens = new HashSet<RefreshToken>();
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string Role { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime CreatedAt { get; }

        public IEnumerable<RefreshToken> RefreshTokens
        {
            get => _refreshTokens;
            protected set => _refreshTokens = new HashSet<RefreshToken>(value);
        }

        protected User()
        {
        }

        public User(Guid aggregateId, string firstName, string lastName, string email,
            string passwordHash, string role)
        {
            AggregateId = aggregateId;
            SetFirstName(firstName);
            SetLastName(lastName);
            SetEmail(email);
            SetRole(role);
            PasswordHash = passwordHash;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetFirstName(string firstName)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                throw new DomainException(Codes.FirstNameNotProvided,
                    $"First name not provided.");
            }

            if (firstName.Length > 150)
            {
                throw new DomainException(Codes.InvalidFirstName,
                    $"First name is too long.");
            }

            if (FirstName == firstName.ToLowerInvariant())
            {
                return;
            }

            FirstName = firstName.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetLastName(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
            {
                throw new DomainException(Codes.LastNameNotProvided,
                    $"Last name not provided.");
            }

            if (lastName.Length > 150)
            {
                throw new DomainException(Codes.InvalidLastName,
                    $"Last name is too long.");
            }

            if (LastName == lastName.ToLowerInvariant())
            {
                return;
            }

            LastName = lastName.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new DomainException(Codes.EmailNotProvided,
                    $"Email not provided.");
            }

            if (!EmailRegex.IsMatch(email))
            {
                throw new DomainException(Codes.InvalidEmail,
                    $"Invalid email: '{email}'.");
            }

            if (email.Length > 300)
            {
                throw new DomainException(Codes.InvalidEmail,
                    $"Email is too long.");
            }

            if (Email == email.ToLowerInvariant())
            {
                return;
            }

            Email = email.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }


        public void SetRole(string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                throw new DomainException(Codes.RoleNotProvided,
                    $"Role not provided.");
            }

            if (role.Length > 30)
            {
                throw new DomainException(Codes.InvalidRole,
                    $"Role name is too long.");
            }

            if (!Enumerations.Role.IsValid(role))
            {
                throw new DomainException(Codes.InvalidRole,
                    $"Invalid role: '{role}'.");
            }

            if (Role == role.ToLowerInvariant())
            {
                return;
            }

            Role = role.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string password, IPasswordHasher<User> passwordHasher)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new DomainException(Codes.InvalidPassword,
                    "Password can not be empty.");
            }

            PasswordHash = passwordHasher.HashPassword(this, password);
        }

        public bool ValidatePassword(string password, IPasswordHasher<User> passwordHasher)
        {
            return passwordHasher.VerifyHashedPassword(this, PasswordHash, password) !=
                   PasswordVerificationResult.Failed;
        }
    }
}