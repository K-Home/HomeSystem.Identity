using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using HomeSystem.Services.Identity.Domain.ValueObjects;
using HomeSystem.Services.Identity.Exceptions;
using KShared.ClassExtensions;
using KShared.Domain.BaseClasses;
using KShared.Exceptions.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace HomeSystem.Services.Identity.Domain.Aggregates
{
    public class User : AggregateRoot, IEditable, ITimestampable
    {
        private ISet<RefreshToken> _refreshTokens = new HashSet<RefreshToken>();
        private ISet<UserSession> _userSessions = new HashSet<UserSession>();
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public UserAddress Address { get; private set; }
        public string PasswordHash { get; private set; }
        public string Role { get; private set; }
        public bool TwoFactorAuthentication { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public IEnumerable<RefreshToken> RefreshTokens
        {
            get => _refreshTokens;
            protected set => _refreshTokens = new HashSet<RefreshToken>(value);
        }
        
        public IEnumerable<UserSession> UserSessions
        {
            get => _userSessions;
            protected set => _userSessions = new HashSet<UserSession>(value);
        }

        protected User()
        {
        }

        public User(Guid aggregateId, string firstName, string lastName, string email, string role)
        {
            AggregateId = aggregateId;
            SetFirstName(firstName);
            SetLastName(lastName);
            SetEmail(email);
            SetRole(role);
            CreatedAt = DateTime.UtcNow;
        }

        public void SetFirstName(string firstName)
        {
            if (firstName.IsEmpty())
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
            if (lastName.IsEmpty())
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
            if (email.IsEmpty())
            {
                throw new DomainException(Codes.EmailNotProvided,
                    $"Email not provided.");
            }

            if (!email.IsEmail())
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

        public void SetAddress(UserAddress address)
        {
            if (address == null)
            {
                throw new DomainException(Codes.AddressNotProvided,
                    "Address can not be null.");
            }

            Address = address;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetRole(string role)
        {
            if (role.IsEmpty())
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

        public void EnableTwoFactorAuthentication()
        {
            TwoFactorAuthentication = true;
            UpdatedAt = DateTime.UtcNow;
        }
        
        public void DisableTwoFactorAuthentication()
        {
            TwoFactorAuthentication = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            if (!phoneNumber.IsPhoneNumber())
            {
                throw new DomainException(Codes.InvalidPhoneNumber,
                    "Invalid phone number");             
            }

            if (PhoneNumber == phoneNumber)
            {
                return;
            }

            PhoneNumber = phoneNumber;
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