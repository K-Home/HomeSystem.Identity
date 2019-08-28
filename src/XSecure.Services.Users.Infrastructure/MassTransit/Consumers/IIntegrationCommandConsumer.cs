using MassTransit;
using XSecure.Services.Users.Infrastructure.Messages;

namespace XSecure.Services.Users.Infrastructure.MassTransit.Consumers
{
    public interface IIntegrationCommandConsumer<in TIntegrationCommand> : IConsumer<TIntegrationCommand> 
        where TIntegrationCommand : class, IIntegrationCommand
    {
    }
}
