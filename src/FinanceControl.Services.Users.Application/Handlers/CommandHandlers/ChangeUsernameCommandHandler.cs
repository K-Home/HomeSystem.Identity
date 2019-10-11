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
    internal sealed class ChangeUsernameCommandHandler : AsyncRequestHandler<ChangeUsernameCommand>
    {
        private readonly IHandler _handler;
        private readonly IMediatRBus _mediatRBus;
        private readonly IUserService _userService;

        public ChangeUsernameCommandHandler(IHandler handler, IMediatRBus mediatRBus,
            IUserService userService)
        {
            _handler = handler.CheckIfNotEmpty();
            _mediatRBus = mediatRBus.CheckIfNotEmpty();
            _userService = userService.CheckIfNotEmpty();
        }

        protected override async Task Handle(ChangeUsernameCommand command, CancellationToken cancellationToken)
        {
            await _handler
                .Run(async () =>
                {
                    await _userService.ChangeNameAsync(command.UserId, command.Name);
                    await _userService.SaveChangesAsync(cancellationToken);
                })
                .OnSuccess(async () =>
                {
                    await _mediatRBus.PublishAsync(
                        new UsernameChangedDomainEvent(command.Request.Id, command.UserId, command.Name),
                        cancellationToken);
                })
                .OnCustomError(async customException =>
                {
                    await _mediatRBus.PublishAsync(
                        new ChangeUsernameRejectedDomainEvent(command.Request.Id, command.UserId, command.Name,
                            customException.Code, customException.Message), cancellationToken);
                })
                .OnError(async (exception, logger) =>
                {
                    logger.Error(exception, $"Error when activating account for user with id: {command.UserId}.", exception);
                    await _mediatRBus.PublishAsync(
                        new ChangeUsernameRejectedDomainEvent(command.Request.Id, command.UserId, command.Name,
                            Codes.Error, exception.Message), cancellationToken);
                })
                .ExecuteAsync();
        }
    }
}