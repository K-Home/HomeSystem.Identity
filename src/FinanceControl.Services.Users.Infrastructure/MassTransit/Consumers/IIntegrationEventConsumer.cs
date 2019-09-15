using FinanceControl.Services.Users.Infrastructure.Messages;
using MassTransit;

namespace FinanceControl.Services.Users.Infrastructure.MassTransit.Consumers
{
    public interface IIntegrationEventConsumer<in TIntegrationEvent> : IConsumer<TIntegrationEvent>
        where TIntegrationEvent : class, IIntegrationEvent
    {
    }
}
