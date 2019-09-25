using System.Runtime.Serialization;
using MediatR;

namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public interface ICommand : IRequest
    {
        Request Request { get; }
    }
}