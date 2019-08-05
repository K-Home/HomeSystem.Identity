using System;
using System.Threading.Tasks;
using HomeSystem.Services.Identity.Application.Exceptions;
using HomeSystem.Services.Identity.Application.Services.Base;
using HomeSystem.Services.Identity.Domain;
using HomeSystem.Services.Identity.Domain.Aggregates;
using HomeSystem.Services.Identity.Domain.Repositories;
using HomeSystem.Services.Identity.Domain.Services;

namespace HomeSystem.Services.Identity.Application.Services
{
    public class OneTimeSecuredOperationService : IOneTimeSecuredOperationService
    {
        private readonly IOneTimeSecuredOperationRepository _oneTimeSecuredOperationRepository;
        private readonly IEncrypter _encrypter;

        public OneTimeSecuredOperationService(IOneTimeSecuredOperationRepository oneTimeSecuredOperationRepository,
            IEncrypter encrypter)
        {
            _oneTimeSecuredOperationRepository = oneTimeSecuredOperationRepository ?? throw new ArgumentNullException(nameof(oneTimeSecuredOperationRepository));
            _encrypter = encrypter ?? throw new ArgumentNullException(nameof(encrypter));
        }

        public async Task<OneTimeSecuredOperation> GetAsync(Guid id)
            => await _oneTimeSecuredOperationRepository.GetAsync(id);

        public async Task CreateAsync(Guid id, string type, string user, DateTime expiry)
        {
            var token = _encrypter.GetRandomSecureKey();
            var operation = new OneTimeSecuredOperation(id, type, user, token, expiry);
            await _oneTimeSecuredOperationRepository.AddAsync(operation);
        }

        public async Task<bool> CanBeConsumedAsync(string type, string user, string token)
        {
            var operation = await _oneTimeSecuredOperationRepository
                .GetAsync(type, user, token);

            return operation != null && operation.CanBeConsumed();
        }

        public async Task ConsumeAsync(string type, string user, string token)
        {
            var operation = await _oneTimeSecuredOperationRepository
                .GetAsync(type, user, token);

            if (operation == null)
            {
                throw new ServiceException(Codes.OperationNotFound,
                    "Operation has not been found.");
            }

            operation.Consume();
            _oneTimeSecuredOperationRepository.Update(operation);
        }
    }
}