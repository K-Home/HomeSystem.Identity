using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using XSecure.Services.Users.Infrastructure.Messages;

namespace XSecure.Services.Users.Infrastructure.MassTransit.MassTransitBus
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
