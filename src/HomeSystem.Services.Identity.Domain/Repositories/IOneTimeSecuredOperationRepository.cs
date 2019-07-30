using HomeSystem.Services.Identity.Domain.Aggregates;
using HomeSystem.Services.Identity.Domain.SeedWork;
using System;
using System.Threading.Tasks;

namespace HomeSystem.Services.Identity.Domain.Repositories
{
    public interface IOneTimeSecuredOperationRepository : IRepository<OneTimeSecuredOperation>
    {
        Task<OneTimeSecuredOperation> GetAsync(Guid id);
        Task<OneTimeSecuredOperation> GetAsync(string type, string user, string token);
        Task AddAsync(OneTimeSecuredOperation operation);
        void Update(OneTimeSecuredOperation operation);
    }
}