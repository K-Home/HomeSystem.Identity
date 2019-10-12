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
    internal sealed class SignOutCommandHandler : AsyncRequestHandler<SignOutCommand>
    {
        private readonly IHandler _handler;
        private readonly IMediatRBus _mediatRBus;
        private readonly IAuthenticationService _authenticationService;

        public SignOutCommandHandler(IHandler handler, 
            IMediatRBus mediatRBus, IAuthenticationService authenticationService)
        {
            _handler = handler.CheckIfNotEmpty();
            _mediatRBus = mediatRBus.CheckIfNotEmpty();
            _authenticationService = authenticationService.CheckIfNotEmpty();
        }

        protected override async Task Handle(SignOutCommand command, CancellationToken cancellationToken)
        {
            await _handler
                .Run(async () =>
                {
                    await _authenticationService.SignOutAsync(command.SessionId, command.UserId);
                    await _authenticationService.SaveChangesAsync(cancellationToken);
                })
                .OnSuccess(async () =>
                    await _mediatRBus.PublishAsync(
                        new SignedOutDomainEvent(command.Request.Id, command.UserId), cancellationToken))
                .OnCustomError(async customException =>
                    await _mediatRBus.PublishAsync(new SignOutRejectedDomainEvent(
                            command.Request.Id, command.UserId, customException.Message, customException.Code),
                        cancellationToken))
                .OnError(async (exception, logger) =>
                {
                    logger.Error($"Error occured while signing out user with id: {command.UserId}.", exception);
                    await _mediatRBus.PublishAsync(
                        new SignOutRejectedDomainEvent(command.Request.Id, command.UserId, exception.Message,
                            Codes.Error), cancellationToken);
                })
                .ExecuteAsync();
        }
    }
}