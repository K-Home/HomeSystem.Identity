using System.Threading;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Application.Messages.Commands;
using FinanceControl.Services.Users.Application.Services.Base;
using FinanceControl.Services.Users.Domain.Extensions;
using FinanceControl.Services.Users.Infrastructure.Handlers;
using MediatR;

namespace FinanceControl.Services.Users.Application.Handlers.CommandHandlers
{
    internal sealed class RefreshUserSessionCommandHandler : AsyncRequestHandler<RefreshUserSessionCommand>
    {
        private readonly IHandler _handler;
        private readonly IAuthenticationService _authenticationService;

        public RefreshUserSessionCommandHandler(IHandler handler, IAuthenticationService authenticationService)
        {
            _handler = handler.CheckIfNotEmpty();
            _authenticationService = authenticationService.CheckIfNotEmpty();
        }

        protected override async Task Handle(RefreshUserSessionCommand command, CancellationToken cancellationToken)
        {
            await _handler
                .Run(async () =>
                {
                    await _authenticationService.RefreshSessionAsync(command.SessionId, command.NewSessionId,
                        command.Key, command.Request.IpAddress, command.Request.UserAgent);

                    await _authenticationService.SaveChangesAsync(cancellationToken);
                })
                .OnError((exception, logger) => { logger.Error($"Error when refreshing session for user", exception); })
                .ExecuteAsync();
        }
    }
}
