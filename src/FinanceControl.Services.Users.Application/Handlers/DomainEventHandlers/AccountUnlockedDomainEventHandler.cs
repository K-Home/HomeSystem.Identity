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
    internal sealed class AccountUnlockedDomainEventHandler : INotificationHandler<AccountUnlockedDomainEvent>
    {
        private readonly ILogger<AccountUnlockedDomainEventHandler> _logger;
        private readonly IMassTransitBusService _massTransitBusService;

        public AccountUnlockedDomainEventHandler(ILogger<AccountUnlockedDomainEventHandler> logger,
            IMassTransitBusService massTransitBusService)
        {
            _logger = logger.CheckIfNotEmpty();
            _massTransitBusService = massTransitBusService.CheckIfNotEmpty();
        }

        public async Task Handle(AccountUnlockedDomainEvent @event, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Handling domain event {DomainEventName} ({@Event})",
                @event.GetGenericTypeName(), @event);

            await _massTransitBusService.PublishAsync(
                new AccountUnlockedIntegrationEvent(@event.RequestId, @event.UserId, @event.LockedUserId,
                    $"Account for user with id: {@event.LockedUserId} has been successfully unlocked."),
                cancellationToken);

            _logger.LogInformation("----- Domain event {DomainEvent} handled", @event.GetGenericTypeName());
        }
    }
}