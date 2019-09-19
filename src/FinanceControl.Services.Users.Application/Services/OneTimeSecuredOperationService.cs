using System;
using System.Threading;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Application.Exceptions;
using FinanceControl.Services.Users.Application.Services.Base;
using FinanceControl.Services.Users.Domain;
using FinanceControl.Services.Users.Domain.Aggregates;
using FinanceControl.Services.Users.Domain.Extensions;
using FinanceControl.Services.Users.Domain.Repositories;
using FinanceControl.Services.Users.Domain.Services;

namespace FinanceControl.Services.Users.Application.Services
{
    public class OneTimeSecuredOperationService : IOneTimeSecuredOperationService
    {
        private readonly IOneTimeSecuredOperationRepository _oneTimeSecuredOperationRepository;
        private readonly IEncrypter _encrypter;

        public OneTimeSecuredOperationService(IOneTimeSecuredOperationRepository oneTimeSecuredOperationRepository,
            IEncrypter encrypter)
        {
            _oneTimeSecuredOperationRepository = oneTimeSecuredOperationRepository.CheckIfNotEmpty();
            _encrypter = encrypter.CheckIfNotEmpty();
        }

        public async Task<OneTimeSecuredOperation> GetAsync(Guid id)
        {
            return await _oneTimeSecuredOperationRepository.GetAsync(id);
        }

        public async Task CreateAsync(Guid id, string type, Guid userId, DateTime expiry)
        {
            var token = _encrypter.GetRandomSecureKey();
            var operation = new OneTimeSecuredOperation(id, type, userId, token, expiry);
            await _oneTimeSecuredOperationRepository.AddAsync(operation);
        }

        public async Task<bool> CanBeConsumedAsync(string type, Guid userId, string token)
        {
            var operation = await _oneTimeSecuredOperationRepository
                .GetAsync(type, userId, token);

            return operation.HasValue() && operation.CanBeConsumed();
        }

        public async Task ConsumeAsync(string type, Guid userId, string token)
        {
            var operation = await _oneTimeSecuredOperationRepository
                .GetAsync(type, userId, token);

            if (operation.HasNoValue())
            {
                throw new ServiceException(Codes.OperationNotFound,
                    "Operation has not been found.");
            }

            operation.Consume();
            _oneTimeSecuredOperationRepository.Update(operation);
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _oneTimeSecuredOperationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}