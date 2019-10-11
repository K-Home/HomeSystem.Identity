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
    internal sealed class DeleteAccountCommandHandler : AsyncRequestHandler<DeleteAccountCommand>
    {
        private readonly IHandler _handler;
        private readonly IMediatRBus _mediatRBus;
        private readonly IUserService _userService;

        public DeleteAccountCommandHandler(IHandler handler, IMediatRBus mediatRBus, IUserService userService)
        {
            _handler = handler.CheckIfNotEmpty();
            _mediatRBus = mediatRBus.CheckIfNotEmpty();
            _userService = userService.CheckIfNotEmpty();
        }

        protected override async Task Handle(DeleteAccountCommand command, CancellationToken cancellationToken)
        {
            await _handler
                .Run(async () =>
                {
                    await _userService.DeleteAsync(command.UserId, command.Soft);
                    await _userService.SaveChangesAsync(cancellationToken);
                })
                .OnSuccess(async () =>
                {
                    await _mediatRBus.PublishAsync(
                        new AccountDeletedDomainEvent(command.Request.Id, command.UserId, command.Soft),
                        cancellationToken);
                })
                .OnCustomError(async customException =>
                {
                    await _mediatRBus.PublishAsync(
                        new DeleteAccountRejectedDomainEvent(command.Request.Id, command.UserId, command.Soft,
                            customException.Code, customException.Message), cancellationToken);
                })
                .OnError(async (exception, logger) =>
                {
                    logger.Error("Error when deleting account.");
                    await _mediatRBus.PublishAsync(
                        new DeleteAccountRejectedDomainEvent(command.Request.Id, command.UserId, command.Soft,
                            Codes.Error, exception.Message), cancellationToken);
                })
                .ExecuteAsync();
        }
    }
}