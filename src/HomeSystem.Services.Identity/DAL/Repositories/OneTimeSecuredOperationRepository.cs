using System;
using System.Threading.Tasks;
using HomeSystem.Services.Identity.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace HomeSystem.Services.Identity.DAL.Repositories
{
    public class OneTimeSecuredOperationRepository : IOneTimeSecuredOperationRepository
    {
        private readonly IdentityDbContext _identityDbContext;

        public OneTimeSecuredOperationRepository(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<OneTimeSecuredOperation> GetAsync(Guid id)
            => await _identityDbContext.OneTimeSecuredOperations.SingleOrDefaultAsync(otso => otso.Id == id);

        public async Task<OneTimeSecuredOperation> GetAsync(string type, string user, string token)
            => await _identityDbContext.OneTimeSecuredOperations.SingleOrDefaultAsync(otso =>
                otso.Type == type && otso.User == user && otso.Token == token);

        public async Task AddAsync(OneTimeSecuredOperation operation)
        {
            await _identityDbContext.OneTimeSecuredOperations.AddAsync(operation);
            await _identityDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(OneTimeSecuredOperation operation)
        {
            _identityDbContext.OneTimeSecuredOperations.Update(operation);
            await _identityDbContext.SaveChangesAsync();
        }
    }
}