using System.Linq;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Domain.Types;
using FinanceControl.Services.Users.Infrastructure.EF;
using MediatR;

namespace FinanceControl.Services.Users.Infrastructure.MediatR
{
    internal static class MediatRExtensions
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, AuthorizationDbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<TrackedObject>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await mediator.Publish(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}