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
    public class CreateActivateAccountSecureOperationRejectedDomainEventHandler :
        INotificationHandler<CreateActivateAccountSecuredOperationRejectedDomainEvent>
    {
        private readonly ILogger<CreateActivateAccountSecureOperationRejectedDomainEventHandler> _logger;
        private readonly IMassTransitBusService _massTransitBusService;

        public CreateActivateAccountSecureOperationRejectedDomainEventHandler(
            ILogger<CreateActivateAccountSecureOperationRejectedDomainEventHandler> logger,
            IMassTransitBusService massTransitBusService)
        {
            _logger = logger.CheckIfNotEmpty();
            _massTransitBusService = massTransitBusService.CheckIfNotEmpty();
        }

        public async Task Handle(CreateActivateAccountSecuredOperationRejectedDomainEvent @event,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Handling domain event {DomainEventName} ({@Event})",
                @event.GetGenericTypeName(), @event);

            await _massTransitBusService.PublishAsync(
                new CreateActivateAccountSecuredOperationRejectedIntegrationEvent(@event.RequestId, @event.UserId,
                    @event.OperationId,
                    $"Created secured operation for user with id: {@event.UserId} rejected, because exception was thrown",
                    @event.Reason, @event.Code), cancellationToken);

            _logger.LogInformation("----- Domain event {DomainEvent} handled", @event.GetGenericTypeName());
        }
    }
}