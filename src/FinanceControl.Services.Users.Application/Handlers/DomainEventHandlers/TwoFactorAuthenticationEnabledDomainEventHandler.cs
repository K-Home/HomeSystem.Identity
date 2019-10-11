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
        TwoFactorAuthenticationEnabledDomainEventHandler : INotificationHandler<
            TwoFactorAuthenticationEnabledDomainEvent>
    {
        private readonly ILogger<TwoFactorAuthenticationEnabledDomainEventHandler> _logger;
        private readonly IMassTransitBusService _massTransitBusService;

        public TwoFactorAuthenticationEnabledDomainEventHandler(
            ILogger<TwoFactorAuthenticationEnabledDomainEventHandler> logger,
            IMassTransitBusService massTransitBusService)
        {
            _logger = logger.CheckIfNotEmpty();
            _massTransitBusService = massTransitBusService.CheckIfNotEmpty();
        }

        public async Task Handle(TwoFactorAuthenticationEnabledDomainEvent @event, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Handling domain event {DomainEventName} ({@Event})",
                @event.GetGenericTypeName(), @event);

            await _massTransitBusService.PublishAsync(
                new TwoFactorAuthenticationEnabledIntegrationEvent(@event.RequestId, @event.UserId,
                    $"Successfully enabled two factor authentication for user with id: {@event.UserId}."),
                cancellationToken);
            
            _logger.LogInformation("----- Domain event {DomainEvent} handled", @event.GetGenericTypeName());
        }
    }
}