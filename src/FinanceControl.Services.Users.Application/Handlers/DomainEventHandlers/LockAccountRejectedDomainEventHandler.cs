using System.Threading;
using System.Threading.Tasks;
using FinanceControl.IntegrationMessages;
using FinanceControl.Services.Users.Application.Messages.DomainEvents;
using FinanceControl.Services.Users.Domain.Extensions;
using FinanceControl.Services.Users.Infrastructure.Extensions;
using FinanceControl.Services.Users.Infrastructure.MassTransit.MassTransitBus;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinanceControl.Services.Users.Application.Handlers.DomainEventHandlers
{
    internal sealed class LockAccountRejectedDomainEventHandler : INotificationHandler<LockAccountRejectedDomainEvent>
    {
        private readonly ILogger<LockAccountRejectedDomainEventHandler> _logger;
        private readonly IMassTransitBusService _massTransitBusService;

        public LockAccountRejectedDomainEventHandler(ILogger<LockAccountRejectedDomainEventHandler> logger,
            IMassTransitBusService massTransitBusService)
        {
            _logger = logger.CheckIfNotEmpty();
            _massTransitBusService = massTransitBusService.CheckIfNotEmpty();
        }

        public async Task Handle(LockAccountRejectedDomainEvent @event, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Handling domain event {DomainEventName} ({@Event})",
                @event.GetGenericTypeName(), @event);

            await _massTransitBusService.PublishAsync(
                new LockAccountRejectedIntegrationEvent(@event.RequestId, @event.UserId, @event.LockedUserId,
                    $"Lock account for user with id: {@event.LockedUserId} failed.", @event.Code, @event.Reason),
                cancellationToken);

            _logger.LogInformation("----- Domain event {DomainEvent} handled", @event.GetGenericTypeName());
        }
    }
}