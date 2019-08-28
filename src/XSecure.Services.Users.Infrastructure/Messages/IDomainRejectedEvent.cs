using MediatR;

namespace XSecure.Services.Users.Infrastructure.Messages
{
    public interface IDomainRejectedEvent : INotification
    {
        string Reason { get; }
        string Code { get; }
    }
}
