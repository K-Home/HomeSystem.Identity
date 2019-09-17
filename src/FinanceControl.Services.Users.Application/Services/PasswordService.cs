using System;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Application.Exceptions;
using FinanceControl.Services.Users.Application.Services.Base;
using FinanceControl.Services.Users.Domain;
using FinanceControl.Services.Users.Domain.Enumerations;
using FinanceControl.Services.Users.Domain.Repositories;
using FinanceControl.Services.Users.Domain.Services;

namespace FinanceControl.Services.Users.Application.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly IUserRepository _userRepository;
        private readonly IOneTimeSecuredOperationService _oneTimeSecuredOperationService;
        private readonly IEncrypter _encrypter;

        public PasswordService(IUserRepository userRepository,
            IOneTimeSecuredOperationService oneTimeSecuredOperationService,
            IEncrypter encrypter)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _oneTimeSecuredOperationService = oneTimeSecuredOperationService ??
                                              throw new ArgumentNullException(nameof(oneTimeSecuredOperationService));
            _encrypter = encrypter ?? throw new ArgumentNullException(nameof(encrypter));
        }

        public async Task ChangeAsync(Guid userId, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetByUserIdAsync(userId);

            if (user == null)
            {
                throw new ServiceException(Codes.UserNotFound,
                    $"User with id: '{userId}' has not been found.");
            }

            if (!user.ValidatePassword(currentPassword, _encrypter))
            {
                throw new ServiceException(Codes.CurrentPasswordIsInvalid,
                    "Current password is invalid.");
            }

            user.SetPassword(newPassword, _encrypter);
            _userRepository.EditUser(user);
        }

        public async Task ResetAsync(Guid operationId, string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
            {
                throw new ServiceException(Codes.UserNotFound,
                    $"User with email: '{email}' has not been found.");
            }

            await _oneTimeSecuredOperationService.CreateAsync(operationId, OneTimeSecuredOperations.ResetPassword,
                user.Id, DateTime.UtcNow.AddDays(1));
        }

        public async Task SetNewAsync(string email, string token, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
            {
                throw new ServiceException(Codes.UserNotFound,
                    $"User with email: '{email}' has not been found.");
            }

            await _oneTimeSecuredOperationService.ConsumeAsync(OneTimeSecuredOperations.ResetPassword,
                user.Id, token);

            user.SetPassword(password, _encrypter);
            _userRepository.EditUser(user);
        }
    }
}