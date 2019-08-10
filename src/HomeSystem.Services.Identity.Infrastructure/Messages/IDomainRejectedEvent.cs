using MediatR;

namespace HomeSystem.Services.Identity.Infrastructure.Messages
{
    public interface IDomainRejectedEvent : INotification
    {
        string Reason { get; }
        string Code { get; }
    }
}
