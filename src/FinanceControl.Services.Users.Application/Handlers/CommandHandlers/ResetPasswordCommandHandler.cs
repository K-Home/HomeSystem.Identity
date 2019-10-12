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
    internal sealed class ResetPasswordCommandHandler : AsyncRequestHandler<ResetPasswordCommand>
    {
        private readonly IHandler _handler;
        private readonly IMediatRBus _mediatRBus;
        private readonly IPasswordService _passwordService;

        public ResetPasswordCommandHandler(IHandler handler,
            IMediatRBus mediatRBus, IPasswordService passwordService)
        {
            _passwordService = passwordService;
            _handler = handler.CheckIfNotEmpty();
            _mediatRBus = mediatRBus.CheckIfNotEmpty();
        }

        protected override async Task Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
        {
            var operationId = Guid.NewGuid();

            await _handler
                .Run(async () =>
                {
                    await _passwordService.ResetAsync(operationId, command.Email);
                    await _passwordService.SaveChangesAsync(cancellationToken);
                })
                .OnSuccess(async () =>
                {
                    await _mediatRBus.PublishAsync(
                        new ResetPasswordInitiatedDomainEvent(command.Request, operationId, command.Email,
                            command.Endpoint), cancellationToken);
                })
                .OnCustomError(async customException =>
                {
                    await _mediatRBus.PublishAsync(
                        new ResetPasswordRejectedDomainEvent(command.Request.Id, command.Email,
                            customException.Message, customException.Code), cancellationToken);
                })
                .OnError(async (exception, logger) =>
                {
                    logger.Error($"Error occured when resetting a password for user with email {command.Email}",
                        exception);
                    await _mediatRBus.PublishAsync(
                        new ResetPasswordRejectedDomainEvent(command.Request.Id, command.Email,
                            exception.Message, Codes.Error), cancellationToken);
                })
                .ExecuteAsync();
        }
    }
}