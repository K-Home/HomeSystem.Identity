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
    internal sealed class UnlockAccountRejectedDomainEventHandler : INotificationHandler<UnlockAccountRejectedDomainEvent>
    {
        private readonly ILogger<UnlockAccountRejectedDomainEventHandler> _logger;
        private readonly IMassTransitBusService _massTransitBusService;

        public UnlockAccountRejectedDomainEventHandler(ILogger<UnlockAccountRejectedDomainEventHandler> logger,
            IMassTransitBusService massTransitBusService)
        {
            _logger = logger.CheckIfNotEmpty();
            _massTransitBusService = massTransitBusService.CheckIfNotEmpty();
        }

        public async Task Handle(UnlockAccountRejectedDomainEvent @event, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Handling domain event {DomainEventName} ({@Event})",
                @event.GetGenericTypeName(), @event);

            await _massTransitBusService.PublishAsync(
                new UnlockAccountRejectedIntegrationEvent(@event.RequestId, @event.UserId, @event.LockedUserId,
                    $"Unlocking account for user with id: {@event.LockedUserId} failed.", @event.Reason, @event.Code),
                cancellationToken);

            _logger.LogInformation("----- Domain event {DomainEvent} handled", @event.GetGenericTypeName());
        }
    }
}