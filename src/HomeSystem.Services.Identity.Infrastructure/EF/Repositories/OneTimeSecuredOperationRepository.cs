using HomeSystem.Services.Identity.Domain.Aggregates;
using HomeSystem.Services.Identity.Domain.Repositories;
using HomeSystem.Services.Identity.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace HomeSystem.Services.Identity.Infrastructure.EF.Repositories
{
    public class OneTimeSecuredOperationRepository : IOneTimeSecuredOperationRepository
    {
        private readonly IdentityDbContext _identityDbContext;

        public IUnitOfWork UnitOfWork => _identityDbContext;

        public OneTimeSecuredOperationRepository(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext ?? throw new ArgumentNullException(nameof(identityDbContext));
        }

        public async Task<OneTimeSecuredOperation> GetAsync(Guid id)
            => await _identityDbContext.OneTimeSecuredOperations.SingleOrDefaultAsync(otso => otso.Id == id);

        public async Task<OneTimeSecuredOperation> GetAsync(string type, Guid userId, string token)
            => await _identityDbContext.OneTimeSecuredOperations.SingleOrDefaultAsync(otso =>
                otso.Type == type && otso.UserId == userId && otso.Token == token);

        public async Task AddAsync(OneTimeSecuredOperation operation)
        {
            await _identityDbContext.OneTimeSecuredOperations.AddAsync(operation);
        }

        public void Update(OneTimeSecuredOperation operation)
        {
            _identityDbContext.Entry(operation).State = EntityState.Modified;
        }
    }
}