using HomeSystem.Services.Identity.Application.Exceptions;
using HomeSystem.Services.Identity.Application.Extensions;
using HomeSystem.Services.Identity.Application.Services.Base;
using HomeSystem.Services.Identity.Domain;
using HomeSystem.Services.Identity.Domain.Aggregates;
using HomeSystem.Services.Identity.Domain.Enumerations;
using HomeSystem.Services.Identity.Domain.Extensions;
using HomeSystem.Services.Identity.Domain.Repositories;
using HomeSystem.Services.Identity.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HomeSystem.Services.Identity.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;
        private readonly IOneTimeSecuredOperationService _securedOperationService;

        public UserService(IUserRepository userRepository,
            IEncrypter encrypter,
            IOneTimeSecuredOperationService securedOperationService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _encrypter = encrypter ?? throw new ArgumentNullException(nameof(encrypter));
            _securedOperationService = securedOperationService ?? throw new ArgumentNullException(nameof(securedOperationService));
        }

        public async Task<bool> IsNameAvailableAsync(string name)
            => await _userRepository.ExistsAsync(x => x.Username == name) == false;

        public async Task<User> GetAsync(Guid userId)
            => await _userRepository.GetByUserIdAsync(userId);

        public async Task<User> GetByNameAsync(string name)
            => await _userRepository.GetByNameAsync(name);

        public async Task<User> GetByEmailAsync(string email)
            => await _userRepository.GetByEmailAsync(email);

        public async Task<string> GetStateAsync(Guid userId)
            => await _userRepository.GetStateAsync(userId);

        public async Task<IEnumerable<User>> BrowseAsync()
            => await _userRepository.GetUsers();

        public async Task SignUpAsync(Guid userId, string email, string role,
            string password = null, bool activate = true, string name = null, 
            string firstName = null, string lastName = null)
        {
            var user = await _userRepository.GetByUserIdAsync(userId);

            if (user != null)
            {
                throw new ServiceException(Codes.UserIdInUse,
                    $"User with id: '{userId}' already exists.");
            }

            user = await _userRepository.GetByEmailAsync(email);

            if (user != null)
            {
                throw new ServiceException(Codes.EmailInUse,
                    $"User with email: {email} already exists!");
            }

            user = await _userRepository.GetByNameAsync(name);

            if (user != null)
            {
                throw new ServiceException(Codes.UserNameInUse,
                    $"User with name: {name} already exists!");
            }

            if (!Roles.IsValid(role))
            {
                throw new ServiceException(Codes.InvalidRole,
                    $"Can not create a new account for user id: '{userId}', invalid role: '{role}'.");
            }

            user = new User(userId, email, role);

            if (!password.IsEmpty())
                user.SetPassword(password, _encrypter);

            if (name.IsNotEmpty() && firstName.IsNotEmpty() && lastName.IsNotEmpty())
            {
                user.SetUserName(name);
                user.SetFirstName(firstName);
                user.SetLastName(lastName);

                if (activate)
                    user.Activate();
                else
                    user.SetUnconfirmed();
            }

            await _userRepository.AddUserAsync(user);
        }

        public async Task ChangeNameAsync(Guid userId, string name)
        {
            var user = await GetAsync(userId);
            if (user == null)
            {
                throw new ServiceException(Codes.UserNotFound,
                    $"User with id: '{userId}' has not been found.");
            }
            if (await IsNameAvailableAsync(name) == false)
            {
                throw new ServiceException(Codes.UserNameInUse,
                    $"User with name: '{name}' already exists.");
            }
            user.SetUserName(name);
            user.Activate();
            _userRepository.EditUser(user);
        }

        public async Task ActivateAsync(string email, string token)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                throw new ServiceException(Codes.UserNotFound,
                    $"User with email: '{email}' has not been found.");
            }

            await _securedOperationService.ConsumeAsync(
                OneTimeSecuredOperations.ActivateAccount, user.Id, token);

            user.Activate();
            _userRepository.EditUser(user);
        }

        public async Task LockAsync(Guid userId)
        {
            var user = await _userRepository.GetOrThrowAsync(userId);

            if (user.Role == Roles.Owner)
            {
                throw new ServiceException(Codes.OwnerCannotBeLocked,
                    $"Owner account: '{userId}' can not be locked.");
            }

            user.Lock();
            _userRepository.EditUser(user);
        }

        public async Task UnlockAsync(Guid userId)
        {
            var user = await _userRepository.GetOrThrowAsync(userId);
            user.Unlock();
            _userRepository.EditUser(user);
        }

        public async Task DeleteAsync(Guid userId, bool soft)
        {
            var user = await _userRepository.GetOrThrowAsync(userId);

            if (soft)
            {
                user.MarkAsDeleted();
                _userRepository.EditUser(user);

                return;
            }

            _userRepository.DeleteUser(user);
        }

        public async Task EnabledTwoFactorAuthorization(Guid userId)
        {
            var user = await _userRepository.GetOrThrowAsync(userId);
            user.EnableTwoFactorAuthentication();
            _userRepository.EditUser(user);
        }

        public async Task DisableTwoFactorAuthorization(Guid userId)
        {
            var user = await _userRepository.GetOrThrowAsync(userId);
            user.DisableTwoFactorAuthentication();
            _userRepository.EditUser(user);
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
            => await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}