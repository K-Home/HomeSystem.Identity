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
    public class SignUpRejectedDomainEventHandler : INotificationHandler<SignUpRejectedDomainEvent>
    {
        private readonly ILogger<SignUpRejectedDomainEventHandler> _logger;
        private readonly IMassTransitBusService _massTransitBusService;

        public SignUpRejectedDomainEventHandler(ILogger<SignUpRejectedDomainEventHandler> logger,
            IMassTransitBusService massTransitBusService)
        {
            _logger = logger.CheckIfNotEmpty();
            _massTransitBusService = massTransitBusService.CheckIfNotEmpty();
        }

        public async Task Handle(SignUpRejectedDomainEvent @event, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Handling domain event {DomainEventName} ({@Event})",
                @event.GetGenericTypeName(), @event);

            await _massTransitBusService.PublishAsync(new SignUpRejectedIntegrationEvent(@event.RequestId,
                @event.UserId, $"Sign up for user with id: {@event.UserId} rejected, because exception was thrown.",
                @event.Code, @event.Reason), cancellationToken);

            _logger.LogInformation("----- Domain event {DomainEvent} handled", @event.GetGenericTypeName());
        }
    }
}