using System.Threading;
using System.Threading.Tasks;
using FinanceControl.IntegrationMessages;
using FinanceControl.Services.Users.Application.Messages.Commands;
using FinanceControl.Services.Users.Application.Messages.DomainEvents;
using FinanceControl.Services.Users.Domain.Extensions;
using FinanceControl.Services.Users.Infrastructure.Extensions;
using FinanceControl.Services.Users.Infrastructure.MassTransit.MassTransitBus;
using FinanceControl.Services.Users.Infrastructure.MediatR.Bus;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinanceControl.Services.Users.Application.Handlers.DomainEventHandlers
{
    internal class SignedUpDomainEventHandler : INotificationHandler<SignedUpDomainEvent>
    {
        private readonly ILogger<SignedUpDomainEventHandler> _logger;
        private readonly IMassTransitBusService _massTransitBusService;
        private readonly IMediatRBus _mediatRBus;

        public SignedUpDomainEventHandler(ILogger<SignedUpDomainEventHandler> logger,
            IMassTransitBusService massTransitBusService, IMediatRBus mediatRBus)
        {
            _logger = logger.CheckIfNotEmpty();
            _massTransitBusService = massTransitBusService.CheckIfNotEmpty();
            _mediatRBus = mediatRBus.CheckIfNotEmpty();
        }

        public async Task Handle(SignedUpDomainEvent @event, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Handling domain event {DomainEventName} ({@Event})",
                @event.GetGenericTypeName(), @event);

            await _mediatRBus.SendAsync(
                new SendActivateAccountMessageCommand(@event.Request, @event.User.Email, @event.User.Username,
                    @event.User.Id), cancellationToken);

            await _massTransitBusService.PublishAsync(new SignedUpIntegrationEvent(@event.Request.Id, @event.User.Id,
                    $"Successfully signed up user with id: {@event.User.Id}.", @event.User.Role, @event.User.State),
                cancellationToken);

            _logger.LogInformation("----- Domain event {DomainEvent} handled", @event.GetGenericTypeName());
        }
    }
}