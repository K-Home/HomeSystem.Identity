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
    internal class SignedInDomainEventHandler : INotificationHandler<SignedInDomainEvent>,
        INotificationHandler<SignInRejectedDomainEvent>
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

        public async Task Handle(SignInRejectedDomainEvent @event, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Handling domain event {DomainEventName} ({@Event})",
                @event.GetGenericTypeName(), @event);

            await _massTransitBusService.PublishAsync(
                new SignInRejectedIntegrationEvent(@event.RequestId, @event.UserId,
                    $"Logged in for user with id: {@event.UserId}, rejected, because exception was thrown.",
                    @event.Reason, @event.Code), cancellationToken);

            _logger.LogInformation("----- Domain event {DomainEvent} handled", @event.GetGenericTypeName());
        }
    }
}