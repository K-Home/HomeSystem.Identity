using MediatR;
using System.Threading;
using System.Threading.Tasks;
using HomeSystem.Services.Identity.Infrastructure.Messages;

namespace HomeSystem.Services.Identity.Infrastructure.MediatR.Bus
{
    public class MediatRBus : IMediatRBus
    {
        private readonly IMediator _mediator;

        public MediatRBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default(CancellationToken))
            where TCommand : IRequest
        {
            await _mediator.Send(command, cancellationToken);
        }

        public async Task<TResult> QueryAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default(CancellationToken)) where TQuery : IQuery<TResult>
        {
            return await _mediator.Send(query, cancellationToken);
        }

        public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default(CancellationToken)) 
            where TEvent : INotification
        {
            await _mediator.Publish(@event, cancellationToken);
        }
    }
}
