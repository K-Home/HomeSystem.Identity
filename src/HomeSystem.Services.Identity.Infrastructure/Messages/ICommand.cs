using MediatR;

namespace HomeSystem.Services.Identity.Infrastructure.Messages
{
    public interface ICommand : IRequest
    {
        Request Request { get; }
    }
}
