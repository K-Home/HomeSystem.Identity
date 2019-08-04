using MediatR;

namespace HomeSystem.Services.Identity.Infrastructure.Messages
{
    public interface ICommand : IRequest<bool>
    {
        Request Request { get; }
    }
}
