using HomeSystem.Services.Identity.Infrastructure.Messages;
using MassTransit;

namespace HomeSystem.Services.Identity.Infrastructure.MassTransit.Consumers
{
    public interface IIntegrationCommandConsumer<in TIntegrationCommand> : IConsumer<TIntegrationCommand> 
        where TIntegrationCommand : class, IIntegrationCommand
    {
    }
}
