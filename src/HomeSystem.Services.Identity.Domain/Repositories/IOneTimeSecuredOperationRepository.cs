using HomeSystem.Services.Identity.Domain.Aggregates;
using System;
using System.Threading.Tasks;

namespace HomeSystem.Services.Identity.Domain.Repositories
{
    public interface IOneTimeSecuredOperationRepository
    {
        Task<OneTimeSecuredOperation> GetAsync(Guid id);
        Task<OneTimeSecuredOperation> GetAsync(string type, string user, string token);
        Task AddAsync(OneTimeSecuredOperation operation);
        Task UpdateAsync(OneTimeSecuredOperation operation);
    }
}