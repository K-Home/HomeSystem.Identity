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
    internal sealed class
        SetNewPasswordRejectedDomainEventHandler : INotificationHandler<SetNewPasswordRejectedDomainEvent>
    {
        private readonly ILogger<SetNewPasswordRejectedDomainEventHandler> _logger;
        private readonly IMassTransitBusService _massTransitBusService;

        public SetNewPasswordRejectedDomainEventHandler(ILogger<SetNewPasswordRejectedDomainEventHandler> logger,
            IMassTransitBusService massTransitBusService)
        {
            _logger = logger.CheckIfNotEmpty();
            _massTransitBusService = massTransitBusService.CheckIfNotEmpty();
        }

        public async Task Handle(SetNewPasswordRejectedDomainEvent @event, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Handling domain event {DomainEventName} ({@Event})",
                @event.GetGenericTypeName(), @event);

            await _massTransitBusService.PublishAsync(
                new SetNewPasswordRejectedIntegrationEvent(@event.RequestId, @event.Code, @event.Reason, @event.Email,
                    $"Set password for user with email: {@event.Email} failed."), cancellationToken);

            _logger.LogInformation("----- Domain event {DomainEvent} handled", @event.GetGenericTypeName());
        }
    }
}