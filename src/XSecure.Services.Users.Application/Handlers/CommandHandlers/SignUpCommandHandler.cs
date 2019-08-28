using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using XSecure.IntegrationMessages.IntegrationEvents;
using XSecure.Services.Users.Application.Messages.Commands;
using XSecure.Services.Users.Application.Messages.DomainEvents;
using XSecure.Services.Users.Application.Services.Base;
using XSecure.Services.Users.Domain;
using XSecure.Services.Users.Domain.Enumerations;
using XSecure.Services.Users.Domain.Extensions;
using XSecure.Services.Users.Infrastructure.Handlers;
using XSecure.Services.Users.Infrastructure.MassTransit.MassTransitBus;
using XSecure.Services.Users.Infrastructure.MediatR.Bus;

namespace XSecure.Services.Users.Application.Handlers.CommandHandlers
{
    public class SignUpCommandHandler : AsyncRequestHandler<SignUpCommand>
    {
        private readonly IHandler _handler;
        private readonly IMassTransitBusService _massTransitBusService;
        private readonly IMediatRBus _mediatRBus;
        private readonly IUserService _userService;
        private readonly IResourceService _resourceService;

        public SignUpCommandHandler(IHandler handler, IMassTransitBusService massTransitBusService, IMediatRBus mediatRBus,
            IUserService userService, IResourceService resourceService)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(massTransitBusService));
            _massTransitBusService = massTransitBusService ?? throw new ArgumentNullException(nameof(massTransitBusService));
            _mediatRBus = mediatRBus ?? throw new ArgumentNullException(nameof(mediatRBus));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _resourceService = resourceService ?? throw new ArgumentNullException(nameof(resourceService));
        }

        protected override async Task Handle(SignUpCommand command, CancellationToken cancellationToken)
        {
            var userId = Guid.NewGuid();
            var resource = _resourceService.Resolve<SignedUpDomainEvent>(userId);

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
                    await _mediatRBus.PublishAsync(
                        new SignedUpDomainEvent(command.Request.Id, userId, "Operation is created and waiting for completion.",
                            resource, command.Role, command.State), cancellationToken);
                })
                .OnCustomError(async customException =>
                {
                    await _mediatRBus.PublishAsync(
                        new SignedUpRejectedDomainEvent(command.Request.Id, userId,
                            "Operation is rejected, because custom exception thrown.", resource,
                            customException.Message, customException.Code), cancellationToken);
                })
                .OnError(async (exception, logger) =>
                {
                    logger.Error("Error occured while signing up a user.", exception);
                    await _mediatRBus.PublishAsync(
                        new SignedUpRejectedDomainEvent(command.Request.Id, userId,
                            "Operation is rejected, because exception thrown.", resource, exception.Message,
                            Codes.Error), cancellationToken);
                })
                .ExecuteAsync();
        }
    }
}
