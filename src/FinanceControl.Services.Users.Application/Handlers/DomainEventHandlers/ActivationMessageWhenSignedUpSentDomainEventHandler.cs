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
    internal class ActivationMessageSentDomainEventHandler :
        INotificationHandler<ActivateAccountSecuredOperationCreatedDomainEvent>
    {
        private readonly ILogger<ActivationMessageSentDomainEventHandler> _logger;
        private readonly IMassTransitBusService _massTransitBusService;

        public ActivationMessageSentDomainEventHandler(
            ILogger<ActivationMessageSentDomainEventHandler> logger,
            IMassTransitBusService massTransitBusService)
        {
            _logger = logger.CheckIfNotEmpty();
            _massTransitBusService = massTransitBusService.CheckIfNotEmpty();
        }

        public async Task Handle(ActivateAccountSecuredOperationCreatedDomainEvent @event,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Handling domain event {DomainEventName} ({@Event})",
                @event.GetGenericTypeName(), @event);

            await _massTransitBusService.SendAsync(new SendActivateAccountMessageIntegrationCommand(@event.Request,
                @event.Email, @event.Username, @event.Token, @event.Endpoint), cancellationToken);

            await _massTransitBusService.PublishAsync(
                new ActivateAccountSecuredOperationCreatedIntegrationEvent(@event.Request.Id, @event.UserId,
                    @event.OperationId, $"Successfully created secured operation for user with id: {@event.UserId}."),
                cancellationToken);

            _logger.LogInformation("----- Domain event {DomainEvent} handled", @event.GetGenericTypeName());
        }
    }
}