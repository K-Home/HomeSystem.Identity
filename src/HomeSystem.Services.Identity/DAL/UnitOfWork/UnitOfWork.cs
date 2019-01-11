using System;
using System.Threading.Tasks;

namespace HomeSystem.Services.Identity.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IdentityDbContext _identityDbContext;

        public UnitOfWork(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task ExecuteAsync(Func<Task> query)
        {
            using (var transaction = await _identityDbContext.Database.BeginTransactionAsync())
            {
                await query();
                transaction.Commit();
            }
        }
    }
}