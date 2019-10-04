using System;

namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public interface ISessionCommand : ICommand
    {
        Guid SessionId { get; }
    }
}