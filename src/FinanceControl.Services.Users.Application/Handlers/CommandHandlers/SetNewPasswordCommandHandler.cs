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
    internal sealed class SetNewPasswordCommandHandler : AsyncRequestHandler<SetNewPasswordCommand>
    {
        private readonly IHandler _handler;
        private readonly IMediatRBus _mediatRBus;
        private readonly IPasswordService _passwordService;

        public SetNewPasswordCommandHandler(IHandler handler,
            IMediatRBus mediatRBus, IPasswordService passwordService)
        {
            _handler = handler.CheckIfNotEmpty();
            _mediatRBus = mediatRBus.CheckIfNotEmpty();
            _passwordService = passwordService.CheckIfNotEmpty();
        }

        protected override async Task Handle(SetNewPasswordCommand command, CancellationToken cancellationToken)
        {
            await _handler
                .Run(async () =>
                {
                    await _passwordService.SetNewAsync(command.Email, command.Token, command.Password);
                    await _passwordService.SaveChangesAsync(cancellationToken);
                })
                .OnSuccess(async () =>
                {
                    await _mediatRBus.PublishAsync(new NewPasswordSetDomainEvent(command.Request.Id, command.Email),
                        cancellationToken);
                })
                .OnCustomError(async customException =>
                {
                    await _mediatRBus.PublishAsync(
                        new SetNewPasswordRejectedDomainEvent(command.Request.Id, customException.Code,
                            customException.Message, command.Email), cancellationToken);
                })
                .OnError(async (exception, logger) =>
                {
                    logger.Error($"Error occured when setting a new password user with email: {command.Email}.",
                        exception);
                    await _mediatRBus.PublishAsync(
                        new SetNewPasswordRejectedDomainEvent(command.Request.Id, Codes.Error,
                            exception.Message, command.Email), cancellationToken);
                })
                .ExecuteAsync();
        }
    }
}