using System.Threading;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Infrastructure.Messages;
using MediatR;

namespace FinanceControl.Services.Users.Infrastructure.MediatR.Bus
{
    public interface IMediatRBus
    {
        Task SendAsync<TCommand>(TCommand command) 
            where TCommand : IRequest;
        
        Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken)
            where TCommand : IRequest;

        Task<TResult> QueryAsync<TQuery, TResult>(TQuery command) where TQuery : IQuery<TResult>;

        Task<TResult> QueryAsync<TQuery, TResult>(TQuery command, CancellationToken cancellationToken)
            where TQuery : IQuery<TResult>;

        Task PublishAsync<TEvent>(TEvent @event)
            where TEvent : INotification;
        
        Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken)
            where TEvent : INotification;
    }
}