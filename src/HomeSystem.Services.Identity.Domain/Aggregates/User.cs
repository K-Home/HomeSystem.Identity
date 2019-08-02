using HomeSystem.Services.Identity.Domain.Exceptions;
using HomeSystem.Services.Identity.Domain.Extensions;
using HomeSystem.Services.Identity.Domain.Types;
using HomeSystem.Services.Identity.Domain.Types.Base;
using HomeSystem.Services.Identity.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using HomeSystem.Services.Identity.Domain.Enumerations;
using HomeSystem.Services.Identity.Domain.Services;

namespace HomeSystem.Services.Identity.Domain.Aggregates
{
    public class User : AggregateRootBase, IEditable, ITimestampable
    {
        private List<UserSession> _userSessions;
        
        public string Username { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public UserAddress Address { get; private set; }
        public string Password { get; private set; }
        public string Salt { get; private set; }
        public Role Role { get; private set; }
        public States State { get; private set; }
        public bool TwoFactorAuthentication { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime CreatedAt { get; }
            
        public IEnumerable<UserSession> UserSessions => _userSessions.AsReadOnly();

        protected User()
        {
            _userSessions = new List<UserSession>();
        }

        public User(Guid id, string email, Role role)
        {
            Id = id;
            Username = $"user-{Id:N}";
            SetEmail(email);
            SetRole(role);
            State = States.Incomplete;        
            
            CreatedAt = DateTime.UtcNow;
        }

        public void SetFirstName(string firstName)
        {
            if (!firstName.IsName())
            {
                throw new DomainException(Codes.InvalidFirstName,
                    $"Invalid first name.");
            }
            
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
            if (lastName.IsName())
            {
                throw new DomainException(Codes.InvalidLastName,
                    $"Invalid last name.");
            }
            
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

        public void SetRole(Role role)
        {
            if (role.Name.IsEmpty())
            {
                throw new DomainException(Codes.RoleNotProvided,
                    $"Role not provided.");
            }

            if (role.Name.Length > 30)
            {
                throw new DomainException(Codes.InvalidRole,
                    $"Role name is too long.");
            }

            if (Equals(Role, role))
            {
                return;
            }

            Role = role;
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

        public void Lock()
        {
            if (Equals(State, States.Locked))
            {
                throw new DomainException(Codes.UserAlreadyLocked, 
                    $"User with id: '{Id}' was already locked.");
            }
            State = States.Locked;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Unlock()
        {
            if (!Equals(State, States.Locked))
            {
                throw new DomainException(Codes.UserNotLocked, 
                    $"User with id: '{Id}' is not locked.");
            }
            State = States.Active;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Activate()
        {
            if (Equals(State, States.Active))
            {
                throw new DomainException(Codes.UserAlreadyActive, 
                    $"User with id: '{Id}' was already activated.");
            }
            State = States.Active;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetUnconfirmed()
        {
            if (Equals(State, States.Unconfirmed))
            {
                throw new DomainException(Codes.UserAlreadyUnconfirmed, 
                    $"User with id: '{Id}' was already set as unconfirmed.");
            }
            State = States.Unconfirmed;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsDeleted()
        {
            if (Equals(State, States.Active))
            {
                throw new DomainException(Codes.UserAlreadyDeleted, 
                    $"User with id: '{Id}' was already marked as deleted.");
            }
            State = States.Deleted;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string password, IEncrypter encrypter)
        {
            if (password.IsEmpty())
            {
                throw new DomainException(Codes.InvalidPassword,
                    "Password can not be empty.");
            }
            if (password.Length < 4)
            {
                throw new DomainException(Codes.InvalidPassword,
                    "Password must contain at least 4 characters.");

            }
            if (password.Length > 100)
            {
                throw new DomainException(Codes.InvalidPassword,
                    "Password can not contain more than 100 characters.");
            }

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