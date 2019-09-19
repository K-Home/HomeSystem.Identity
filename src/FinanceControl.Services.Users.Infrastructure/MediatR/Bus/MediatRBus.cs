using System.Threading;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Infrastructure.Messages;
using MediatR;

namespace FinanceControl.Services.Users.Infrastructure.MediatR.Bus
{
    public class MediatRBus : IMediatRBus
    {
        private readonly IMediator _mediator;

        public MediatRBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendAsync<TCommand>(TCommand command)
            where TCommand : IRequest
        {
            await _mediator.Send(command);
        }

        public async Task SendAsync<TCommand>(TCommand command,
            CancellationToken cancellationToken)
            where TCommand : IRequest
        {
            await _mediator.Send(command, cancellationToken);
        }

        public async Task<TResult> QueryAsync<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>
        {
            return await _mediator.Send(query);
        }

        public async Task<TResult> QueryAsync<TQuery, TResult>(TQuery query,
            CancellationToken cancellationToken) where TQuery : IQuery<TResult>
        {
            return await _mediator.Send(query, cancellationToken);
        }

        public async Task PublishAsync<TEvent>(TEvent @event)
            where TEvent : INotification
        {
            await _mediator.Publish(@event);
        }

        public async Task PublishAsync<TEvent>(TEvent @event,
            CancellationToken cancellationToken) where TEvent : INotification
        {
            await _mediator.Publish(@event, cancellationToken);
        }
    }
}