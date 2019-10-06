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
    internal class SignInCommandHandler : AsyncRequestHandler<SignInCommand>
    {
        private readonly IHandler _handler;
        private readonly IMediatRBus _mediatRBus;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;

        public SignInCommandHandler(IHandler handler, IMediatRBus mediatRBus,
            IAuthenticationService authenticationService, IUserService userService)
        {
            _handler = handler.CheckIfNotEmpty();
            _mediatRBus = mediatRBus.CheckIfNotEmpty();
            _authenticationService = authenticationService.CheckIfNotEmpty();
            _userService = userService.CheckIfNotEmpty();
        }

        protected override async Task Handle(SignInCommand command, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByEmailAsync(command.Email);

            await _handler
                .Run(async () =>
                {
                    await _authenticationService.SignInAsync(command.SessionId, command.Email,
                        command.Password, command.IpAddress, command.UserAgent);

                    await _authenticationService.SaveChangesAsync(cancellationToken);
                })
                .OnSuccess(async () =>
                    await _mediatRBus.PublishAsync(new SignedInDomainEvent(
                        command.Request.Id, user.Id, user.Email, user.Username), cancellationToken))
                .OnCustomError(async customException =>
                    await _mediatRBus.PublishAsync(
                        new SignInRejectedDomainEvent(
                            command.Request.Id, user.Id, customException.Code, customException.Message),
                        cancellationToken))
                .OnError(async (exception, logger) =>
                {
                    logger.Error($"Error occured while logging in a user wid id: {user.Id}.", exception);
                    await _mediatRBus.PublishAsync(
                        new SignInRejectedDomainEvent(command.Request.Id, user.Id, Codes.Error, exception.Message),
                        cancellationToken);
                })
                .ExecuteAsync();
        }
    }
}