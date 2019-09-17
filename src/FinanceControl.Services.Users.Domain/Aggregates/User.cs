using System;
using System.Collections.Generic;
using FinanceControl.Services.Users.Domain.Enumerations;
using FinanceControl.Services.Users.Domain.Exceptions;
using FinanceControl.Services.Users.Domain.Extensions;
using FinanceControl.Services.Users.Domain.Services;
using FinanceControl.Services.Users.Domain.Types;
using FinanceControl.Services.Users.Domain.Types.Base;
using FinanceControl.Services.Users.Domain.ValueObjects;

namespace FinanceControl.Services.Users.Domain.Aggregates
{
    public class User : AggregateRootBase, IEditable, ITimestampable
    {
        public Avatar Avatar { get; private set; }
        public string Username { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public UserAddress Address { get; private set; }
        public string Password { get; private set; }
        public string Salt { get; private set; }
        public string Role { get; private set; }
        public string State { get; private set; }
        public bool TwoFactorAuthentication { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public static IEnumerable<UserSession> UserSessions
            => new List<UserSession>();

        public static IEnumerable<OneTimeSecuredOperation> OneTimeSecuredOperations
            => new List<OneTimeSecuredOperation>();

        protected User()
        {
        }

        public User(Guid id, string email, string role)
        {
            Id = id;
            Avatar = Avatar.Empty;
            Address = UserAddress.Empty;
            Username = $"user-{Id:N}";
            SetEmail(email);
            SetRole(role);
            State = States.Incomplete;
            TwoFactorAuthentication = false;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetFirstName(string firstName)
        {
            if (!firstName.IsName())
                throw new DomainException(Codes.FirstNameIsInvalid,
                    $"Invalid first name.");

            if (firstName.IsEmpty())
                throw new DomainException(Codes.FirstNameNotProvided,
                    $"First name not provided.");

            if (firstName.Length > 150)
                throw new DomainException(Codes.FirstNameIsInvalid,
                    $"First name is too long.");

            if (FirstName == firstName.ToLowerInvariant())
            {
                return;
            }

            FirstName = firstName.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetLastName(string lastName)
        {
            if (lastName.IsName())
                throw new DomainException(Codes.LastNameIsInvalid,
                    $"Invalid last name.");

            if (lastName.IsEmpty())
                throw new DomainException(Codes.LastNameNotProvided,
                    $"Last name not provided.");

            if (lastName.Length > 150)
                throw new DomainException(Codes.LastNameIsInvalid,
                    $"Last name is too long.");

            if (LastName == lastName.ToLowerInvariant())
            {
                return;
            }

            LastName = lastName.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetUserName(string name)
        {
            if (!Equals(State, States.Incomplete))
                throw new DomainException(Codes.UserNameAlreadySet,
                    $"User name has been already set: {Username}");

            if (name.IsEmpty())
            {
                throw new ArgumentException("User name can not be empty.", nameof(name));
            }

            if (Username.EqualsCaseInvariant(name))
            {
                return;
            }

            if (name.Length < 2)
            {
                throw new ArgumentException("User name is too short.", nameof(name));
            }

            if (name.Length > 50)
            {
                throw new ArgumentException("User name is too long.", nameof(name));
            }

            if (name.IsName() == false)
                throw new ArgumentException("User name doesn't meet the required criteria.", nameof(name));

            Username = name;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetEmail(string email)
        {
            if (email.IsEmpty())
                throw new DomainException(Codes.EmailNotProvided,
                    $"Email not provided.");

            if (!email.IsEmail())
                throw new DomainException(Codes.EmailIsInvalid,
                    $"Invalid email: '{email}'.");

            if (email.Length > 300)
                throw new DomainException(Codes.EmailIsInvalid,
                    $"Email is too long.");

            if (Email == email.ToLowerInvariant())
            {
                return;
            }

            Email = email.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetAddress(UserAddress address)
        {
            if (Address.Equals(address))
                return;

            Address = address;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetRole(string role)
        {
            if (Role == role)
                return;

            Role = role;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetAvatar(Avatar avatar)
        {
            if (avatar == null)
            {
                return;
            }

            Avatar = avatar;
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveAvatar()
        {
            Avatar = Avatar.Empty;
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
                throw new DomainException(Codes.PhoneNumberIsInvalid,
                    "Invalid phone number");

            if (PhoneNumber == phoneNumber)
            {
                return;
            }

            PhoneNumber = phoneNumber;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Lock()
        {
            if (Equals(State, States.Locked))
                throw new DomainException(Codes.UserAlreadyLocked,
                    $"User with id: '{Id}' was already locked.");

            State = States.Locked;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Unlock()
        {
            if (!Equals(State, States.Locked))
                throw new DomainException(Codes.UserNotLocked,
                    $"User with id: '{Id}' is not locked.");

            State = States.Active;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Activate()
        {
            if (Equals(State, States.Active))
                throw new DomainException(Codes.UserAlreadyActive,
                    $"User with id: '{Id}' was already activated.");

            State = States.Active;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetUnconfirmed()
        {
            if (Equals(State, States.Unconfirmed))
                throw new DomainException(Codes.UserAlreadyUnconfirmed,
                    $"User with id: '{Id}' was already set as unconfirmed.");

            State = States.Unconfirmed;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsDeleted()
        {
            if (Equals(State, States.Active))
                throw new DomainException(Codes.UserAlreadyDeleted,
                    $"User with id: '{Id}' was already marked as deleted.");

            State = States.Deleted;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string password, IEncrypter encrypter)
        {
            if (password.IsEmpty())
                throw new DomainException(Codes.PasswordIsInvalid,
                    "Password can not be empty.");
            if (password.Length < 4)
                throw new DomainException(Codes.PasswordIsInvalid,
                    "Password must contain at least 4 characters.");
            if (password.Length > 100)
                throw new DomainException(Codes.PasswordIsInvalid,
                    "Password can not contain more than 100 characters.");

            var salt = encrypter.GetSalt(password);
            var hash = encrypter.GetHash(password, salt);

            Password = hash;
            Salt = salt;
            UpdatedAt = DateTime.UtcNow;
        }

        public bool ValidatePassword(string password, IEncrypter encrypter)
        {
            var hashedPassword = encrypter.GetHash(password, Salt);
            var areEqual = Password.Equals(hashedPassword);

            return areEqual;
        }
    }
}