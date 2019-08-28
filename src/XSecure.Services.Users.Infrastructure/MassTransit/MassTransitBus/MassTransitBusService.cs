using MassTransit;
using System;
using System.Threading;
using System.Threading.Tasks;
using XSecure.Services.Users.Infrastructure.Messages;

namespace XSecure.Services.Users.Infrastructure.MassTransit.MassTransitBus
{
    public class MassTransitBusService : IMassTransitBusService
    {
        private readonly IBusControl _busControl;
        private readonly IBus _bus;

        public MassTransitBusService(IBusControl busControl, IBus bus)
        {
            _busControl = busControl ?? throw new ArgumentNullException(nameof(busControl));
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }

        public async Task SendAsync<TIntegrationCommand>(TIntegrationCommand integrationCommand,
            CancellationToken cancellationToken = default(CancellationToken))
            where TIntegrationCommand : class, IIntegrationCommand
        {
            await _bus.Publish(integrationCommand, cancellationToken);
        }

        public async Task PublishAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent,
            CancellationToken cancellationToken = default(CancellationToken))
            where TIntegrationEvent : class, IIntegrationEvent
        {
            await _bus.Publish(integrationEvent, cancellationToken);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _busControl.StartAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _busControl.StopAsync(cancellationToken);
        }
    }
}
