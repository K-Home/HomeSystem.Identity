using System;
using System.Threading.Tasks;
using HomeSystem.Services.Identity.Domain.Aggregates;

namespace HomeSystem.Services.Identity.DAL.Repositories
{
    public interface IOneTimeSecuredOperationRepository
    {
        Task<OneTimeSecuredOperation> GetAsync(Guid id);
        Task<OneTimeSecuredOperation> GetAsync(string type, string user, string token);
        Task AddAsync(OneTimeSecuredOperation operation);
        Task UpdateAsync(OneTimeSecuredOperation operation);
    }
}