using HomeSystem.Services.Identity.Application.Messages.DomainEvents;
using HomeSystem.Services.Identity.Infrastructure.MassTransit.MassTransitBus;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using HomeSystem.IntegrationMessages.IntegrationEvents;

namespace HomeSystem.Services.Identity.Application.Handlers.DomainEventHandlers
{
    public class SignedUpDomainEventHandler : INotificationHandler<SignedUp>, 
                                              INotificationHandler<SignedUpRejected>
    {
        private readonly IMassTransitBusService _massTransitBusService;

        public SignedUpDomainEventHandler(IMassTransitBusService massTransitBusService)
        {
            _massTransitBusService =
                massTransitBusService ?? throw new ArgumentNullException(nameof(massTransitBusService));
        }

        public async Task Handle(SignedUp @event, CancellationToken cancellationToken)
        {
            await _massTransitBusService.PublishAsync(new SignedUpIntegrationEvent(@event.RequestId, @event.UserId,
                @event.Resource, @event.Role, @event.State), cancellationToken);
        }

        public async Task Handle(SignedUpRejected @event, CancellationToken cancellationToken)
        {
            await _massTransitBusService.PublishAsync(new SignUpRejectedIntegrationEvent(@event.RequestId, @event.UserId,
                @event.Code, @event.Reason), cancellationToken);
        }
    }
}