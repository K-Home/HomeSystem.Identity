using HomeSystem.Services.Identity.Infrastructure.Messages;
using MassTransit;

namespace HomeSystem.Services.Identity.Infrastructure.MassTransit.Consumers
{
    public interface IIntegrationEventConsumer<in TIntegrationEvent> : IConsumer<TIntegrationEvent>
        where TIntegrationEvent : class, IIntegrationEvent
    {
    }
}
