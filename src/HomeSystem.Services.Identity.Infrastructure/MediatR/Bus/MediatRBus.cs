using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HomeSystem.Services.Identity.Infrastructure.MediatR.Bus
{
    public class MediatRBus : IMediatRBus
    {
        private readonly IMediator _mediator;

        public MediatRBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Send<TCommand>(TCommand command, CancellationToken cancellationToken = default(CancellationToken))
            where TCommand : IRequest
        {
            await _mediator.Send(command, cancellationToken);
        }

        public async Task Publish<TEvent>(TEvent @event, CancellationToken cancellationToken = default(CancellationToken)) 
            where TEvent : INotification
        {
            await _mediator.Publish(@event, cancellationToken);
        }
    }
}
