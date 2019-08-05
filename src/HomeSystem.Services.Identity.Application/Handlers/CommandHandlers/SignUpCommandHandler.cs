using System;
using System.Threading;
using System.Threading.Tasks;
using HomeSystem.Services.Identity.Application.Messages.Commands;
using HomeSystem.Services.Identity.Application.Services.Base;
using HomeSystem.Services.Identity.Domain;
using HomeSystem.Services.Identity.Domain.DomainEvents;
using HomeSystem.Services.Identity.Domain.Exceptions;
using HomeSystem.Services.Identity.Infrastructure.MediatR.Bus;
using MediatR;
using Serilog;

namespace HomeSystem.Services.Identity.Application.Handlers.CommandHandlers
{
    public class SignUpCommandHandler : IRequestHandler<SignUp, bool>
    {
        private static readonly ILogger Logger = Log.Logger;

        private readonly IMediatRBus _mediatRBus;
        private readonly IUserService _userService;

        public SignUpCommandHandler(IMediatRBus mediatRBus, IUserService userService)
        {
            _mediatRBus = mediatRBus ?? throw new ArgumentNullException(nameof(mediatRBus));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<bool> Handle(SignUp command, CancellationToken cancellationToken)
        {
            var userId = Guid.NewGuid();

            try
            {
                await _userService.SignUpAsync(userId, command.Email, command.Role, command.Password, activate: true,
                    name: command.Name, firstName: command.FirstName, lastName: command.LastName);

                return await _userService.SaveChangesAsync(cancellationToken);
            }
            catch(HomeSystemException customException)
            {
                await _mediatRBus.Publish(new SignedUpRejected(command.Request.Id, userId, customException.Message,
                    customException.Code), cancellationToken);

                return false;
            }
            catch(Exception exception)
            {
                Logger.Error("Error occured while signing up a user", exception);
                await _mediatRBus.Publish(new SignedUpRejected(command.Request.Id, userId, exception.Message,
                    Codes.Error), cancellationToken);

                return false;
            }
        }
    }
}
