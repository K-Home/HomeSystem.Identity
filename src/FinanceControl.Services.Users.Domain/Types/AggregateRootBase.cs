using System;
using System.Collections.Generic;
using FinanceControl.Services.Users.Domain.Types.Base;
using MediatR;

namespace FinanceControl.Services.Users.Domain.Types
{
    public abstract class AggregateRootBase : TrackedObject, IAggregateRoot
    {
        public Guid Id { get; protected set; }
        private int? _requestedHashCode;

        public override void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public override void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public override void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public bool IsTransient()
        {
            return Id == default(Guid);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is AggregateRootBase))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            var item = (AggregateRootBase) obj;

            if (item.IsTransient() || IsTransient())
                return false;

            return item.Id == Id;
        }

        public override int GetHashCode()
        {
            if (IsTransient())
                return base.GetHashCode();

            if (!_requestedHashCode.HasValue)
                _requestedHashCode = Id.GetHashCode() ^ 31;

            return _requestedHashCode.Value;
        }

        public static bool operator ==(AggregateRootBase left, AggregateRootBase right)
        {
            if (Equals(left, null))
                return Equals(right, null) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(AggregateRootBase left, AggregateRootBase right)
        {
            return !(left == right);
        }
    }
}