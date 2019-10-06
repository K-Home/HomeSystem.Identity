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
    internal class ActivateAccountCommandHandler : AsyncRequestHandler<ActivateAccountCommand>
    {
        private readonly IHandler _handler;
        private readonly IMediatRBus _mediatRBus;
        private readonly IUserService _userService;

        public ActivateAccountCommandHandler(IHandler handler, IMediatRBus mediatRBus,
            IUserService userService)
        {
            _handler = handler.CheckIfNotEmpty();
            _mediatRBus = mediatRBus.CheckIfNotEmpty();
            _userService = userService.CheckIfNotEmpty();
        }

        protected override async Task Handle(ActivateAccountCommand command, CancellationToken cancellationToken)
        {
            await _handler
                .Run(async () =>
                {
                    await _userService.ActivateAsync(command.Email, command.Token);
                    await _userService.SaveChangesAsync(cancellationToken);
                })
                .OnSuccess(async () =>
                {
                    var user = await _userService.GetByEmailAsync(command.Email);
                    await _mediatRBus.PublishAsync(
                        new AccountActivatedDomainEvent(command.Request.Id, command.Email, user.Id),
                        cancellationToken);
                })
                .OnCustomError(async customException =>
                {
                    await _mediatRBus.PublishAsync(
                        new ActivateAccountRejectedDomainEvent(command.Request.Id, command.Email,
                            customException.Code, customException.Message), cancellationToken);
                })
                .OnError(async (exception, logger) =>
                {
                    logger.Error(exception, "Error when activating account.");
                    await _mediatRBus.PublishAsync(
                        new ActivateAccountRejectedDomainEvent(command.Request.Id, command.Email, Codes.Error,
                            exception.Message), cancellationToken);
                })
                .ExecuteAsync();
        }
    }
}