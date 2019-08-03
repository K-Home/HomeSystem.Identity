using MediatR;

namespace HomeSystem.Services.Domain.DomainEvents.Base
{
    public interface IDomainRejectedEvent : INotification
    {
        string Reason { get; }
        string Code { get; }
    }
}
