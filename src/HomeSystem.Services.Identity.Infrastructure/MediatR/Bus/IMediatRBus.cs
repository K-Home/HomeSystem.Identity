using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace HomeSystem.Services.Identity.Infrastructure.MediatR.Bus
{
    public interface IMediatRBus
    {
        Task Send<TCommand>(TCommand command, CancellationToken cancellationToken = default(CancellationToken)) where TCommand : IRequest;
        Task Publish<TEvent>(TEvent @event, CancellationToken cancellationToken = default(CancellationToken)) where TEvent : INotification;
    }
}
