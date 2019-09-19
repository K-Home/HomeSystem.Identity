using System;
using System.Runtime.Serialization;

namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public interface IAuthenticatedCommand : ICommand
    {
        [DataMember]
        Guid UserId { get; }
    }
}