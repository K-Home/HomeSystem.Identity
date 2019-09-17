using System;
using System.Threading;
using System.Threading.Tasks;

namespace FinanceControl.Services.Users.Domain.SeedWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<bool> SaveEntitiesAsync();
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken);
    }
}