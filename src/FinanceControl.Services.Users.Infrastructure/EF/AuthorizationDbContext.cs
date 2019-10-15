using System;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Domain.Aggregates;
using FinanceControl.Services.Users.Domain.Extensions;
using FinanceControl.Services.Users.Domain.SeedWork;
using FinanceControl.Services.Users.Infrastructure.EF.Configurations;
using FinanceControl.Services.Users.Infrastructure.MediatR;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;

namespace FinanceControl.Services.Users.Infrastructure.EF
{
    public class AuthorizationDbContext : DbContext, IUnitOfWork
    {
        private readonly IOptions<SqlOptions> _sqlOptions;
        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;
        
        public AuthorizationDbContext(IOptions<SqlOptions> sqlOptions, IMediator mediator)
        {
            _mediator = mediator.CheckIfNotEmpty();
            _sqlOptions = sqlOptions.CheckIfNotEmpty();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<OneTimeSecuredOperation> OneTimeSecuredOperations { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }

        public IDbContextTransaction GetCurrentTransaction()
        {
            return _currentTransaction;
        }

        public bool HasActiveTransaction => _currentTransaction.HasValue();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            if (_sqlOptions.Value.InMemory)
            {
                optionsBuilder.UseInMemoryDatabase(_sqlOptions.Value.Database);
                return;
            }

            optionsBuilder.UseSqlServer(_sqlOptions.Value.ConnectionString,
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(AuthorizationDbContext).GetTypeInfo().Assembly.GetName().Name);
                    sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserSessionConfiguration());
            modelBuilder.ApplyConfiguration(new OneTimeSecuredOperationConfiguration());
            modelBuilder.ApplyConfiguration(new AvatarConfiguration());
            modelBuilder.ApplyConfiguration(new UserAddressConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> SaveEntitiesAsync()
        {        
            await _mediator.DispatchDomainEventsAsync(this);
            await base.SaveChangesAsync();
            
            return true;
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken)
        {
            await _mediator.DispatchDomainEventsAsync(this);
            await base.SaveChangesAsync(cancellationToken);
            
            return true;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction.HasValue())
            {
                return null;
            }

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            transaction.ThrowIfNull();

            if (transaction != _currentTransaction)
            {
                throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");
            }

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction.HasValue())
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction.HasValue())
                {
                    _currentTransaction?.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}