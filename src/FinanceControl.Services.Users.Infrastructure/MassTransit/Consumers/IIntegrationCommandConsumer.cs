using FinanceControl.Services.Users.Infrastructure.Messages;
using MassTransit;

namespace FinanceControl.Services.Users.Infrastructure.MassTransit.Consumers
{
    public interface IIntegrationCommandConsumer<in TIntegrationCommand> : IConsumer<TIntegrationCommand>
        where TIntegrationCommand : class, IIntegrationCommand
    {
    }
}