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
    internal sealed class ChangePasswordCommandHandler : AsyncRequestHandler<ChangePasswordCommand>
    {
        private readonly IHandler _handler;
        private readonly IMediatRBus _mediatRBus;
        private readonly IPasswordService _passwordService;

        public ChangePasswordCommandHandler(IHandler handler, IMediatRBus mediatRBus,
            IPasswordService passwordService)
        {
            _handler = handler.CheckIfNotEmpty();
            _mediatRBus = mediatRBus.CheckIfNotEmpty();
            _passwordService = passwordService.CheckIfNotEmpty();
        }


        protected override async Task Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
        {
            await _handler
                .Run(async () =>
                {
                    await _passwordService.ChangeAsync(command.UserId, command.CurrentPassword,
                        command.NewPassword);
                })
                .OnSuccess(async () =>
                {
                    await _mediatRBus.PublishAsync(
                        new PasswordChangedDomainEvent(command.Request.Id, command.UserId),
                        cancellationToken);
                })
                .OnCustomError(async customException =>
                {
                    await _mediatRBus.PublishAsync(
                        new ChangePasswordRejectedDomainEvent(command.Request.Id, command.UserId,
                            customException.Code, customException.Message), cancellationToken);
                })
                .OnError(async (exception, logger) =>
                {
                    logger.Error(exception, $"Error when changing a password for user with id: {command.UserId}.", exception);
                    await _mediatRBus.PublishAsync(
                        new ChangePasswordRejectedDomainEvent(command.Request.Id, command.UserId, Codes.Error,
                            exception.Message), cancellationToken);
                })
                .ExecuteAsync();
        }
    }
}