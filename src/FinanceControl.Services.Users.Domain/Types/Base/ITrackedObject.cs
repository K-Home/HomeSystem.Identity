using MediatR;

namespace FinanceControl.Services.Users.Domain.Types.Base
{
    public interface ITrackedObject : IIdentifiable
    {
        void AddDomainEvent(INotification eventItem);
        void RemoveDomainEvent(INotification eventItem);
        void ClearDomainEvents();
    }
}