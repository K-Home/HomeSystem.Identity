using System;
using System.Threading;
using System.Threading.Tasks;
using FinanceControl.IntegrationMessages;
using FinanceControl.Services.Users.Application.Messages.Commands;
using FinanceControl.Services.Users.Application.Messages.DomainEvents;
using FinanceControl.Services.Users.Application.Services.Base;
using FinanceControl.Services.Users.Domain;
using FinanceControl.Services.Users.Domain.Enumerations;
using FinanceControl.Services.Users.Domain.Extensions;
using FinanceControl.Services.Users.Infrastructure.Handlers;
using FinanceControl.Services.Users.Infrastructure.MassTransit.MassTransitBus;
using FinanceControl.Services.Users.Infrastructure.MediatR.Bus;
using MediatR;

namespace FinanceControl.Services.Users.Application.Handlers.CommandHandlers
{
    public class SignUpCommandHandler : AsyncRequestHandler<SignUpCommand>
    {
        private readonly IHandler _handler;
        private readonly IMassTransitBusService _massTransitBusService;
        private readonly IMediatRBus _mediatRBus;
        private readonly IUserService _userService;
        private readonly IResourceService _resourceService;

        public SignUpCommandHandler(IHandler handler, IMassTransitBusService massTransitBusService,
            IMediatRBus mediatRBus,
            IUserService userService, IResourceService resourceService)
        {
            _handler = handler.CheckIfNotEmpty();
            _massTransitBusService = massTransitBusService.CheckIfNotEmpty();
            _mediatRBus = mediatRBus.CheckIfNotEmpty();
            _userService = userService.CheckIfNotEmpty();
            _resourceService = resourceService.CheckIfNotEmpty();
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

                    await _userService.SignUpAsync(userId, command.Email, command.UserName, command.Password,
                        command.Request.Culture);

                    await _userService.SaveChangesAsync(cancellationToken);
                })
                .OnSuccess(async () =>
                {
                    var user = await _userService.GetAsync(userId);

                    await _mediatRBus.PublishAsync(
                        new SignedUpDomainEvent(command.Request.Id, userId,
                            "Operation is created and waiting for completion.",
                            resource, user.Role, user.State), cancellationToken);
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