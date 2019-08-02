using System;

namespace HomeSystem.Services.Identity.Infrastructure.Messages
{
    public interface IAuthenticatedCommand : ICommand
    {
        Guid UserId { get; }
    }
}
