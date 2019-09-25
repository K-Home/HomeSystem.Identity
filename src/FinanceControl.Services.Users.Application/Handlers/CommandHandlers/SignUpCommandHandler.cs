using System;
using System.Threading;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Application.Messages.Commands;
using FinanceControl.Services.Users.Application.Messages.DomainEvents;
using FinanceControl.Services.Users.Application.Services.Base;
using FinanceControl.Services.Users.Domain;
using FinanceControl.Services.Users.Domain.Extensions;
using FinanceControl.Services.Users.Infrastructure.Handlers;
using FinanceControl.Services.Users.Infrastructure.MediatR.Bus;
using MediatR;

namespace FinanceControl.Services.Users.Application.Handlers.CommandHandlers
{
    internal class SignUpCommandHandler : AsyncRequestHandler<SignUpCommand>
    {
        private readonly IHandler _handler;
        private readonly IMediatRBus _mediatRBus;
        private readonly IUserService _userService;

        public SignUpCommandHandler(IHandler handler, IMediatRBus mediatRBus,
            IUserService userService)
        {
            _handler = handler.CheckIfNotEmpty();
            _mediatRBus = mediatRBus.CheckIfNotEmpty();
            _userService = userService.CheckIfNotEmpty();
        }

        protected override async Task Handle(SignUpCommand command, CancellationToken cancellationToken)
        {
            var userId = Guid.NewGuid();

            await _handler
                .Run(async () =>
                {
                    await _userService.SignUpAsync(userId, command.Email, command.UserName, command.Password,
                        command.Request.Culture);

                    await _userService.SaveChangesAsync(cancellationToken);
                })
                .OnSuccess(async () =>
                {
                    var user = await _userService.GetAsync(userId);

                    await _mediatRBus.PublishAsync(
                        new SignedUpDomainEvent(command.Request, user,
                            $"Successfully signed up user with id: {userId}."),
                        cancellationToken);
                })
                .OnCustomError(async customException =>
                {
                    await _mediatRBus.PublishAsync(
                        new SignUpRejectedDomainEvent(command.Request.Id, userId,
                            "Sign up rejected, because custom exception was thrown.",
                            customException.Message, customException.Code), cancellationToken);
                })
                .OnError(async (exception, logger) =>
                {
                    logger.Error("Error occured while signing up a user.", exception);
                    await _mediatRBus.PublishAsync(
                        new SignUpRejectedDomainEvent(command.Request.Id, userId,
                            "Sign up rejected, because exception was thrown.", exception.Message,
                            Codes.Error), cancellationToken);
                })
                .ExecuteAsync();
        }
    }
}