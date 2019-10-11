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
    internal sealed class LockAccountCommandHandler : AsyncRequestHandler<LockAccountCommand>
    {
        private readonly IHandler _handler;
        private readonly IMediatRBus _mediatRBus;
        private readonly IUserService _userService;

        public LockAccountCommandHandler(IHandler handler, IMediatRBus mediatRBus, IUserService userService)
        {
            _handler = handler.CheckIfNotEmpty();
            _mediatRBus = mediatRBus.CheckIfNotEmpty();
            _userService = userService.CheckIfNotEmpty();
        }

        protected override async Task Handle(LockAccountCommand command, CancellationToken cancellationToken)
        {
            await _handler
                .Run(async () =>
                {
                    await _userService.LockAsync(command.LockUserId);
                    await _userService.SaveChangesAsync(cancellationToken);
                })
                .OnSuccess(async () =>
                {
                    await _mediatRBus.PublishAsync(
                        new AccountLockedDomainEvent(command.Request.Id, command.UserId, command.LockUserId),
                        cancellationToken);
                })
                .OnCustomError(async customException =>
                {
                    await _mediatRBus.PublishAsync(
                        new LockAccountRejectedDomainEvent(command.Request.Id, command.UserId, command.LockUserId,
                            customException.Message, customException.Message), cancellationToken);
                })
                .OnError(async (exception, logger) =>
                {
                    logger.Error($"Error when locking account for user with id: {command.LockUserId}.", exception);
                    await _mediatRBus.PublishAsync(
                        new LockAccountRejectedDomainEvent(command.Request.Id, command.UserId, command.LockUserId,
                            exception.Message, Codes.Error), cancellationToken);
                })
                .ExecuteAsync();
        }
    }
}