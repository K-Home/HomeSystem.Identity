using System.Threading;
using System.Threading.Tasks;
using FinanceControl.IntegrationMessages;
using FinanceControl.Services.Users.Application.Messages.DomainEvents;
using FinanceControl.Services.Users.Application.Services.Base;
using FinanceControl.Services.Users.Domain.Extensions;
using FinanceControl.Services.Users.Infrastructure.Extensions;
using FinanceControl.Services.Users.Infrastructure.MassTransit.MassTransitBus;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinanceControl.Services.Users.Application.Handlers.DomainEventHandlers
{
    internal sealed class ResetPasswordInitiatedDomainEventHandler : INotificationHandler<ResetPasswordInitiatedDomainEvent>
    {
        private readonly ILogger<ResetPasswordInitiatedDomainEventHandler> _logger;
        private readonly IMassTransitBusService _massTransitBusService;
        private readonly IOneTimeSecuredOperationService _oneTimeSecuredOperationService;

        public ResetPasswordInitiatedDomainEventHandler(ILogger<ResetPasswordInitiatedDomainEventHandler> logger,
            IMassTransitBusService massTransitBusService, IOneTimeSecuredOperationService oneTimeSecuredOperationService)
        {
            _logger = logger.CheckIfNotEmpty();
            _massTransitBusService = massTransitBusService.CheckIfNotEmpty();
            _oneTimeSecuredOperationService = oneTimeSecuredOperationService.CheckIfNotEmpty();
        }

        public async Task Handle(ResetPasswordInitiatedDomainEvent @event, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Handling domain event {DomainEventName} ({@Event})",
                @event.GetGenericTypeName(), @event);

            var operation = await _oneTimeSecuredOperationService.GetAsync(@event.OperationId);
            
            await _massTransitBusService.PublishAsync(
                new ResetPasswordInitiatedIntegrationEvent(@event.Request.Id, @event.OperationId, @event.Email, @event.Endpoint,
                    $"Reset password for user with email: {@event.Email} initiated."),
                cancellationToken);

            await _massTransitBusService.SendAsync(
                new SendResetPasswordMessageIntegrationCommand(@event.Request, @event.Email, operation.Token,
                    @event.Endpoint), cancellationToken);

            _logger.LogInformation("----- Domain event {DomainEvent} handled", @event.GetGenericTypeName());
        }
    }
}