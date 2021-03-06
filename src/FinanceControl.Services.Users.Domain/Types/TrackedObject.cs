﻿using System.Collections.Generic;
using MediatR;

namespace FinanceControl.Services.Users.Domain.Types
{
    public abstract class TrackedObject
    {
        protected List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public virtual void AddDomainEvent(INotification eventItem)
        {
        }

        public virtual void RemoveDomainEvent(INotification eventItem)
        {
        }

        public virtual void ClearDomainEvents()
        {
        }
    }
}