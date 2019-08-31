using System;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Domain.Aggregates;
using FinanceControl.Services.Users.Domain.SeedWork;

namespace FinanceControl.Services.Users.Domain.Repositories
{
    public interface IOneTimeSecuredOperationRepository : IRepository<OneTimeSecuredOperation>
    {
        Task<OneTimeSecuredOperation> GetAsync(Guid id);
        Task<OneTimeSecuredOperation> GetAsync(string type, Guid userId, string token);
        Task AddAsync(OneTimeSecuredOperation operation);
        void Update(OneTimeSecuredOperation operation);
    }
}