using System;
using System.Threading.Tasks;
using HomeSystem.Services.Identity.Domain.Aggregates;

namespace HomeSystem.Services.Identity.Application.Services.Base
{
    public interface IOneTimeSecuredOperationService
    {
        Task<OneTimeSecuredOperation> GetAsync(Guid id);
        Task CreateAsync(Guid id, string type, string userEmail, DateTime expiry);
        Task<bool> CanBeConsumedAsync(string type, string userEmail, string token);
        Task ConsumeAsync(string type, string user, string token);
    }
}