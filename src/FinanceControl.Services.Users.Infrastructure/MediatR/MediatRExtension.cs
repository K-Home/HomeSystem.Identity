using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Domain.Types;
using FinanceControl.Services.Users.Infrastructure.EF;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FinanceControl.Services.Users.Infrastructure.MediatR
{
    static class MediatRExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, IdentityDbContext ctx)
        {
            var domainEntities = GetTrackedObjects<EntityBase>(ctx); 
            var domainAggregates = GetTrackedObjects<AggregateRootBase>(ctx);
            var domainEventsFromEntities = GetEvents(domainEntities);
            var domainEventsFromAggregates = GetEvents(domainAggregates);
            var domainEvents = AggregateLists(domainEventsFromEntities, domainEventsFromAggregates);

            ClearDomainEvents(domainEntities, domainAggregates);

            var tasks = domainEvents
                .Select(async domainEvent =>
                {
                    await mediator.Publish(domainEvent);
                });

            await Task.WhenAll(tasks);
        }

        private static List<EntityEntry<T>> GetTrackedObjects<T>(DbContext ctx)
            where T : TrackedObject
        {
            var domainTrackedObjects = ctx.ChangeTracker
                .Entries<T>()
                .Where(x => x.Entity.DomainEvents != null
                            && x.Entity.DomainEvents.Any()).ToList();

            return domainTrackedObjects;
        }

        private static IEnumerable<INotification> GetEvents<T>(IEnumerable<EntityEntry<T>> domainTrackedObjects) 
            where T : TrackedObject
        {
            var domainEvents = domainTrackedObjects
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            return domainEvents;

        }

        private static IEnumerable<INotification> AggregateLists(IEnumerable<INotification> eventsFromEntities, 
            IEnumerable<INotification> domainEventsFromAggregates)
        {
            var domainEvents = new List<INotification>();

            domainEvents
                .AddRange(eventsFromEntities);

            domainEvents
                .AddRange(domainEventsFromAggregates);

            return domainEvents;
        }

        private static void ClearDomainEvents(IEnumerable<EntityEntry<EntityBase>> domainEntities,
            IEnumerable<EntityEntry<AggregateRootBase>> domainAggregates)
        {

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            domainAggregates.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());
        }
    }
}