using System;
using System.Threading;
using System.Threading.Tasks;
using FinanceControl.IntegrationMessages;
using FinanceControl.Services.Users.Application.Messages.DomainEvents;
using FinanceControl.Services.Users.Infrastructure.Extensions;
using FinanceControl.Services.Users.Infrastructure.MassTransit.MassTransitBus;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinanceControl.Services.Users.Application.Handlers.DomainEventHandlers
{
    public class SignedUpDomainEventHandler : INotificationHandler<SignedUpDomainEvent>, 
                                              INotificationHandler<SignedUpRejectedDomainEvent>
    {
        private readonly ILogger<SignedUpDomainEventHandler> _logger;
        private readonly IMassTransitBusService _massTransitBusService;

        public SignedUpDomainEventHandler(ILogger<SignedUpDomainEventHandler> logger, IMassTransitBusService massTransitBusService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _massTransitBusService =
                massTransitBusService ?? throw new ArgumentNullException(nameof(massTransitBusService));
        }

        public async Task Handle(SignedUpDomainEvent @event, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Handling domain event {DomainEventName} ({@Event})", @event.GetGenericTypeName(), @event);

            await _massTransitBusService.PublishAsync(new SignedUpIntegrationEvent(@event.RequestId, @event.UserId,
                @event.Message, @event.Resource, @event.Role, @event.State), cancellationToken);

            _logger.LogInformation("----- Domain event {DomainEvent} handled", @event.GetGenericTypeName());
        }

        public async Task Handle(SignedUpRejectedDomainEvent @event, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Handling domain event {DomainEventName} ({@Event})", @event.GetGenericTypeName(), @event);

            await _massTransitBusService.PublishAsync(new SignUpRejectedIntegrationEvent(@event.RequestId, @event.UserId,
                @event.Message, @event.Resource, @event.Code, @event.Reason), cancellationToken);

            _logger.LogInformation("----- Domain event {DomainEvent} handled", @event.GetGenericTypeName());
        }
    }
}