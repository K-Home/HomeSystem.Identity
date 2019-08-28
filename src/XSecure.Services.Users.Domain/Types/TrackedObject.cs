using MediatR;
using System.Collections.Generic;

namespace XSecure.Services.Users.Domain.Types
{
    public abstract class TrackedObject
    {
        protected List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public virtual void AddDomainEvent(INotification eventItem) { }
        public virtual void RemoveDomainEvent(INotification eventItem) { }
        public virtual void ClearDomainEvents() { }
    }
}
