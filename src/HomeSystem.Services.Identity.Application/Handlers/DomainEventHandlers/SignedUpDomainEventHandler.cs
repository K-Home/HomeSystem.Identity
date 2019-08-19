using HomeSystem.IntegrationMessages.IntegrationEvents;
using HomeSystem.Services.Identity.Application.Messages.DomainEvents;
using HomeSystem.Services.Identity.Infrastructure.Extensions;
using HomeSystem.Services.Identity.Infrastructure.MassTransit.MassTransitBus;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HomeSystem.Services.Identity.Application.Handlers.DomainEventHandlers
{
    public class SignedUpDomainEventHandler : INotificationHandler<SignedUp>, 
                                              INotificationHandler<SignedUpRejected>
    {
        private readonly ILogger<SignedUpDomainEventHandler> _logger;
        private readonly IMassTransitBusService _massTransitBusService;

        public SignedUpDomainEventHandler(ILogger<SignedUpDomainEventHandler> logger, IMassTransitBusService massTransitBusService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _massTransitBusService =
                massTransitBusService ?? throw new ArgumentNullException(nameof(massTransitBusService));
        }

        public async Task Handle(SignedUp @event, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Handling domain event {DomainEventName} ({@Event})", @event.GetGenericTypeName(), @event);

            await _massTransitBusService.PublishAsync(new SignedUpIntegrationEvent(@event.RequestId, @event.UserId,
                @event.Resource, @event.Role, @event.State), cancellationToken);

            _logger.LogInformation("----- Domain event {DomainEvent} handled", @event.GetGenericTypeName());
        }

        public async Task Handle(SignedUpRejected @event, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Handling domain event {DomainEventName} ({@Event})", @event.GetGenericTypeName(), @event);

            await _massTransitBusService.PublishAsync(new SignUpRejectedIntegrationEvent(@event.RequestId, @event.UserId,
                @event.Code, @event.Reason), cancellationToken);

            _logger.LogInformation("----- Domain event {DomainEvent} handled", @event.GetGenericTypeName());
        }
    }
}