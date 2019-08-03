using System;
using System.Threading;
using System.Threading.Tasks;
using HomeSystem.Services.Identity.Application.Messages.Commands;
using HomeSystem.Services.Identity.Application.Services.Base;
using MediatR;

namespace HomeSystem.Services.Identity.Application.Handlers.CommandHandlers
{
    public class SignUpCommandHandler : IRequestHandler<SignUp, bool>
    {
        private readonly IUserService _userService;

        public SignUpCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(SignUp command, CancellationToken cancellationToken)
        {
            var userId = Guid.NewGuid();
            await _userService.SignUpAsync(userId, command.Email, command.Role, command.Password, activate: true,
                name: command.Name, firstName: command.FirstName, lastName: command.LastName);

            return true;
        }
    }
}
