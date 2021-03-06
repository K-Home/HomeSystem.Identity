﻿using System.Threading;
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
    internal sealed class UsernameChangedDomainEventHandler : INotificationHandler<UsernameChangedDomainEvent>
    {
        private readonly ILogger<UsernameChangedDomainEventHandler> _logger;
        private readonly IMassTransitBusService _massTransitBusService;

        public UsernameChangedDomainEventHandler(ILogger<UsernameChangedDomainEventHandler> logger,
            IMassTransitBusService massTransitBusService)
        {
            _logger = logger.CheckIfNotEmpty();
            _massTransitBusService = massTransitBusService.CheckIfNotEmpty();
        }

        public async Task Handle(UsernameChangedDomainEvent @event, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Handling domain event {DomainEventName} ({@Event})",
                @event.GetGenericTypeName(), @event);

            await _massTransitBusService.PublishAsync(new UsernameChangedIntegrationEvent(@event.RequestId,
                    @event.UserId, @event.Name,
                    $"Username for user with id: {@event.UserId} has been changed to the: {@event.Name}"),
                cancellationToken);

            _logger.LogInformation("----- Domain event {DomainEvent} handled", @event.GetGenericTypeName());
        }
    }
}