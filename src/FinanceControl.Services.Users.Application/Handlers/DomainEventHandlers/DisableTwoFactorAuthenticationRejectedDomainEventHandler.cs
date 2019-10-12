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
        DisableTwoFactorAuthenticationRejectedDomainEventHandler : INotificationHandler<
            DisableTwoFactorAuthenticationRejectedDomainEvent>
    {
        private readonly ILogger<DisableTwoFactorAuthenticationRejectedDomainEventHandler> _logger;
        private readonly IMassTransitBusService _massTransitBusService;

        public DisableTwoFactorAuthenticationRejectedDomainEventHandler(
            ILogger<DisableTwoFactorAuthenticationRejectedDomainEventHandler> logger,
            IMassTransitBusService massTransitBusService)
        {
            _logger = logger.CheckIfNotEmpty();
            _massTransitBusService = massTransitBusService.CheckIfNotEmpty();
        }

        public async Task Handle(DisableTwoFactorAuthenticationRejectedDomainEvent @event,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Handling domain event {DomainEventName} ({@Event})",
                @event.GetGenericTypeName(), @event);

            await _massTransitBusService.PublishAsync(
                new DisableTwoFactorAuthenticationRejectedIntegrationEvent(@event.RequestId, @event.UserId,
                    @event.Reason, @event.Code,
                    $"Disable two factor authentication for user with id: {@event.UserId} failed."), cancellationToken);

            _logger.LogInformation("----- Domain event {DomainEvent} handled", @event.GetGenericTypeName());
        }
    }
}