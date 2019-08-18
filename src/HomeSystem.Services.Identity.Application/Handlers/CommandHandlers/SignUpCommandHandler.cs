using HomeSystem.Services.Identity.Application.Messages.Commands;
using HomeSystem.Services.Identity.Application.Messages.DomainEvents;
using HomeSystem.Services.Identity.Application.Services.Base;
using HomeSystem.Services.Identity.Domain;
using HomeSystem.Services.Identity.Domain.Enumerations;
using HomeSystem.Services.Identity.Domain.Extensions;
using HomeSystem.Services.Identity.Infrastructure.Handlers;
using HomeSystem.Services.Identity.Infrastructure.MassTransit.MassTransitBus;
using HomeSystem.Services.Identity.Infrastructure.MediatR.Bus;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using HomeSystem.IntegrationMessages.IntegrationEvents;

namespace HomeSystem.Services.Identity.Application.Handlers.CommandHandlers
{
    public class SignUpCommandHandler : AsyncRequestHandler<SignUpCommand>
    {
        private readonly IHandler _handler;
        private readonly IMassTransitBusService _massTransitBusService;
        private readonly IMediatRBus _mediatRBus;
        private readonly IUserService _userService;
        private readonly IResourceService _resourceService;
        private readonly ILogger<SignUpCommandHandler> _logger;

        public SignUpCommandHandler(IHandler handler, IMassTransitBusService massTransitBusService, IMediatRBus mediatRBus,
            IUserService userService, IResourceService resourceService, ILogger<SignUpCommandHandler> logger)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(massTransitBusService));
            _massTransitBusService = massTransitBusService ?? throw new ArgumentNullException(nameof(massTransitBusService));
            _mediatRBus = mediatRBus ?? throw new ArgumentNullException(nameof(mediatRBus));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _resourceService = resourceService ?? throw new ArgumentNullException(nameof(resourceService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task Handle(SignUpCommand command, CancellationToken cancellationToken)
        {
            var userId = Guid.NewGuid();
            var resource = _resourceService.Resolve<SignedUp>(userId);

            await _handler
                .Run(async () =>
                {
                    await _massTransitBusService.PublishAsync(
                        new SignUpRequestCreatedIntegrationEvent(command.Request.Id, userId, resource, string.Empty),
                        cancellationToken);

                    await _userService.SignUpAsync(userId, command.Email,
                        command.Role.IsEmpty() ? Roles.User : command.Role, command.Password, command.State == "active",
                        command.UserName);

                    await _userService.SaveChangesAsync(cancellationToken);
                })
                .OnSuccess(async () =>
                {
                    await _mediatRBus.PublishAsync(new SignedUp(command.Request.Id, userId, resource, command.Role,
                        command.State), cancellationToken);
                })
                .OnCustomError(async customException =>
                {
                    await _mediatRBus.PublishAsync(new SignedUpRejected(command.Request.Id, userId,
                        customException.Message, customException.Code), cancellationToken);
                })
                .OnError(async (exception, logger) =>
                {
                    logger.Error("Error occured while signing up a user", exception);
                    await _mediatRBus.PublishAsync(new SignedUpRejected(command.Request.Id, userId, exception.Message,
                        Codes.Error), cancellationToken);
                })
                .ExecuteAsync();
        }
    }
}
