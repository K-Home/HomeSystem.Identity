using System;

namespace XSecure.Services.Users.Infrastructure.Messages
{
    public interface IAuthenticatedCommand : ICommand
    {
        Guid UserId { get; }
    }
}
