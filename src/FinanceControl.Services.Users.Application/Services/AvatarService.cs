using System;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Application.Exceptions;
using FinanceControl.Services.Users.Application.Services.Base;
using FinanceControl.Services.Users.Domain;
using FinanceControl.Services.Users.Domain.Aggregates;
using FinanceControl.Services.Users.Domain.Repositories;
using FinanceControl.Services.Users.Domain.ValueObjects;
using FinanceControl.Services.Users.Infrastructure.Files;
using FinanceControl.Services.Users.Infrastructure.Files.Base;

namespace FinanceControl.Services.Users.Application.Services
{
    public class AvatarService : IAvatarService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFileHandler _fileHandler;
        private readonly IImageService _imageService;
        private readonly IFileValidator _fileValidator;

        public AvatarService(IUserRepository userRepository,
            IFileHandler fileHandler, IImageService imageService,
            IFileValidator fileValidator)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _fileHandler = fileHandler ?? throw new ArgumentNullException(nameof(fileHandler));
            _imageService = imageService ?? throw new ArgumentNullException(nameof(imageService));
            _fileValidator = fileValidator ?? throw new ArgumentNullException(nameof(fileValidator));
        }

        public async Task<string> GetUrlAsync(Guid userId)
        {
            var user = await _userRepository.GetByUserIdAsync(userId);
            if (user == null)
            {
                throw new ServiceException(Codes.UserNotFound,
                    $"User with id: '{userId}' has not been found.");
            }

            return user.Avatar?.Url ?? string.Empty;
        }

        public async Task AddOrUpdateAsync(Guid userId, File avatar)
        {
            if (avatar == null)
            {
                throw new ServiceException(Codes.FileIsInvalid,
                    $"There is no avatar file to be uploaded.");
            }

            if (!_fileValidator.IsImage(avatar))
            {
                throw new ServiceException(Codes.FileIsInvalid);
            }

            var user = await _userRepository.GetByUserIdAsync(userId);
            var name = $"avatar_{userId:N}.jpg";
            var resizedAvatar = _imageService.ProcessImage(avatar, 200);
            await RemoveAsync(user, userId);
            await _fileHandler.UploadAsync(resizedAvatar, name,
                (baseUrl, fullUrl) => { user.SetAvatar(Avatar.Create(name, fullUrl)); });

            _userRepository.EditUser(user);
        }

        public async Task RemoveAsync(Guid userId)
        {
            var user = await _userRepository.GetByUserIdAsync(userId);
            await RemoveAsync(user, userId).ConfigureAwait(false);
            _userRepository.EditUser(user);
        }

        private async Task RemoveAsync(User user, Guid userId)
        {
            if (user == null)
            {
                throw new ServiceException(Codes.UserNotFound,
                    $"User with id: '{userId}' has not been found.");
            }

            if (user.Avatar == null)
            {
                return;
            }

            if (user.Avatar.IsEmpty)
            {
                return;
            }

            await _fileHandler.DeleteAsync(user.Avatar.Name);
            user.RemoveAvatar();
        }
    }
}