using System;

namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public interface IAuthenticatedCommand : ICommand
    {
        Guid UserId { get; }
    }
}
