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
    internal sealed class UploadAvatarRejectedDomainEventHandler : INotificationHandler<UploadAvatarRejectedDomainEvent>
    {
        private readonly ILogger<UploadAvatarRejectedDomainEventHandler> _logger;
        private readonly IMassTransitBusService _massTransitBusService;

        public UploadAvatarRejectedDomainEventHandler(ILogger<UploadAvatarRejectedDomainEventHandler> logger,
            IMassTransitBusService massTransitBusService)
        {
            _logger = logger.CheckIfNotEmpty();
            _massTransitBusService = massTransitBusService.CheckIfNotEmpty();
        }

        public async Task Handle(UploadAvatarRejectedDomainEvent @event, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Handling domain event {DomainEventName} ({@Event})",
                @event.GetGenericTypeName(), @event);

            await _massTransitBusService.PublishAsync(new UploadAvatarRejectedIntegrationEvent(@event.RequestId,
                @event.UserId, $"Error occured when uploading avatar for user with id :{@event.UserId}", @event.Code,
                @event.Reason), cancellationToken);

            _logger.LogInformation("----- Domain event {DomainEvent} handled", @event.GetGenericTypeName());
        }
    }
}