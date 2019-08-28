using System;
using System.Threading.Tasks;
using XSecure.Services.Users.Domain.Aggregates;
using XSecure.Services.Users.Domain.SeedWork;

namespace XSecure.Services.Users.Domain.Repositories
{
    public interface IOneTimeSecuredOperationRepository : IRepository<OneTimeSecuredOperation>
    {
        Task<OneTimeSecuredOperation> GetAsync(Guid id);
        Task<OneTimeSecuredOperation> GetAsync(string type, Guid userId, string token);
        Task AddAsync(OneTimeSecuredOperation operation);
        void Update(OneTimeSecuredOperation operation);
    }
}