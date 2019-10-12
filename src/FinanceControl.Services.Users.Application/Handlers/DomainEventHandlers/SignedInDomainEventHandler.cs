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
    internal sealed class SignedInDomainEventHandler : INotificationHandler<SignedInDomainEvent>
    {
        private readonly ILogger<SignedInDomainEventHandler> _logger;
        private readonly IMassTransitBusService _massTransitBusService;

        public SignedInDomainEventHandler(ILogger<SignedInDomainEventHandler> logger,
            IMassTransitBusService massTransitBusService)
        {
            _logger = logger.CheckIfNotEmpty();
            _massTransitBusService = massTransitBusService.CheckIfNotEmpty();
        }

        public async Task Handle(SignedInDomainEvent @event, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Handling domain event {DomainEventName} ({@Event})",
                @event.GetGenericTypeName(), @event);

            await _massTransitBusService.PublishAsync(
                new SignedInIntegrationEvent(@event.RequestId, @event.UserId,
                    $"User with id: {@event.UserId} successfully logged in.", @event.Email, @event.Name),
                cancellationToken);

            _logger.LogInformation("----- Domain event {DomainEvent} handled", @event.GetGenericTypeName());
        }
    }
}