using MediatR;

namespace XSecure.Services.Users.Infrastructure.Messages
{
    public interface ICommand : IRequest
    {
        Request Request { get; }
    }
}
