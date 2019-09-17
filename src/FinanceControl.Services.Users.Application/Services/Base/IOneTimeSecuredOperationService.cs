using System;
using System.Threading;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Domain.Aggregates;

namespace FinanceControl.Services.Users.Application.Services.Base
{
    public interface IOneTimeSecuredOperationService
    {
        Task<OneTimeSecuredOperation> GetAsync(Guid id);
        Task CreateAsync(Guid id, string type, Guid userId, DateTime expiry);
        Task<bool> CanBeConsumedAsync(string type, Guid userId, string token);
        Task ConsumeAsync(string type, Guid userId, string token);
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
    }
}