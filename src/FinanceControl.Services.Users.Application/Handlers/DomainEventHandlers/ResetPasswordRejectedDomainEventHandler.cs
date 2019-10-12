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
        ResetPasswordRejectedDomainEventHandler : INotificationHandler<ResetPasswordRejectedDomainEvent>
    {
        private readonly ILogger<ResetPasswordRejectedDomainEventHandler> _logger;
        private readonly IMassTransitBusService _massTransitBusService;

        public ResetPasswordRejectedDomainEventHandler(ILogger<ResetPasswordRejectedDomainEventHandler> logger,
            IMassTransitBusService massTransitBusService)
        {
            _logger = logger.CheckIfNotEmpty();
            _massTransitBusService = massTransitBusService.CheckIfNotEmpty();
        }

        public async Task Handle(ResetPasswordRejectedDomainEvent @event, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Handling domain event {DomainEventName} ({@Event})",
                @event.GetGenericTypeName(), @event);

            await _massTransitBusService.PublishAsync(
                new ResetPasswordRejectedIntegrationEvent(@event.RequestId, @event.Email,
                    $"Resetting password for user with email: {@event.Email} failed.", @event.Reason, @event.Code),
                cancellationToken);

            _logger.LogInformation("----- Domain event {DomainEvent} handled", @event.GetGenericTypeName());
        }
    }
}