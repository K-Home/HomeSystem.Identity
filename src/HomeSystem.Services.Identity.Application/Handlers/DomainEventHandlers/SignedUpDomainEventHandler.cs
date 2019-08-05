using System.Threading;
using System.Threading.Tasks;
using HomeSystem.Services.Identity.Domain.DomainEvents;
using MediatR;

namespace HomeSystem.Services.Identity.Application.Handlers.DomainEventHandlers
{
    public class SignedUpDomainEventHandler : INotificationHandler<SignedUp>, 
                                              INotificationHandler<SignedUpRejected>
    {
        public Task Handle(SignedUp notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task Handle(SignedUpRejected notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}