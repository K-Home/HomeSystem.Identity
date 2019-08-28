using MassTransit;
using XSecure.Services.Users.Infrastructure.Messages;

namespace XSecure.Services.Users.Infrastructure.MassTransit.Consumers
{
    public interface IIntegrationEventConsumer<in TIntegrationEvent> : IConsumer<TIntegrationEvent>
        where TIntegrationEvent : class, IIntegrationEvent
    {
    }
}
