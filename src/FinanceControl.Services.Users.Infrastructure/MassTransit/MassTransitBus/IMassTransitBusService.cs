using System.Threading;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Microsoft.Extensions.Hosting;

namespace FinanceControl.Services.Users.Infrastructure.MassTransit.MassTransitBus
{
    public interface IMassTransitBusService : IHostedService
    {
        Task SendAsync<TIntegrationCommand>(TIntegrationCommand integrationCommand,
            CancellationToken cancellationToken = default(CancellationToken))
            where TIntegrationCommand : class, IIntegrationCommand;

        Task PublishAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent,
            CancellationToken cancellationToken = default(CancellationToken))
            where TIntegrationEvent : class, IIntegrationEvent;
    }
}