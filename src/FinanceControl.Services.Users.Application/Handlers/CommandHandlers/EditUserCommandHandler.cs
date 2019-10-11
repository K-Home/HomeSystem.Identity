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
    internal sealed class EditUserCommandHandler : AsyncRequestHandler<EditUserCommand>
    {
        private readonly IHandler _handler;
        private readonly IMediatRBus _mediatRBus;
        private readonly IUserService _userService;

        public EditUserCommandHandler(IHandler handler, IMediatRBus mediatRBus, IUserService userService)
        {
            _handler = handler.CheckIfNotEmpty();
            _mediatRBus = mediatRBus.CheckIfNotEmpty();
            _userService = userService.CheckIfNotEmpty();
        }

        protected override async Task Handle(EditUserCommand command, CancellationToken cancellationToken)
        {
            await _handler
                .Run(async () =>
                {
                    await _userService.UpdateAsync(command.UserId, command.Name, command.FirstName,
                        command.LastName, command.Address.Street, command.Address.City, command.Address.State,
                        command.Address.Country, command.Address.ZipCode);
                    await _userService.SaveChangesAsync(cancellationToken);
                })
                .OnSuccess(async () =>
                {
                    await _mediatRBus.PublishAsync(new UserEditedDomainEvent(command.Request.Id, command.UserId),
                        cancellationToken);
                })
                .OnCustomError(async customException =>
                {
                    await _mediatRBus.PublishAsync(
                        new EditUserRejectedDomainEvent(command.Request.Id, command.UserId, customException.Code,
                            customException.Message), cancellationToken);
                })
                .OnError(async (exception, logger) =>
                {
                    logger.Error($"Error when editing user with id: {command.UserId}.", exception);
                    await _mediatRBus.PublishAsync(
                        new EditUserRejectedDomainEvent(command.Request.Id, command.UserId, Codes.Error,
                            exception.Message), cancellationToken);
                })
                .ExecuteAsync();
        }
    }
}