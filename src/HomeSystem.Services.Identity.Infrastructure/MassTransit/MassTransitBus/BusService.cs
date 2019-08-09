using System;
using System.Threading;
using System.Threading.Tasks;
using HomeSystem.Services.Identity.Infrastructure.MassTransit.Options;
using HomeSystem.Services.Identity.Infrastructure.Messages;
using MassTransit;

namespace HomeSystem.Services.Identity.Infrastructure.MassTransit.MassTransitBus
{
    public class BusService : IBusService
    {
        private readonly IBusService _busService;
        private readonly IBus _bus;

        public BusService(IBusService busService, IBus bus)
        {
            _busService = busService ?? throw new ArgumentNullException(nameof(busService));
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
            await _busService.StartAsync(cancellationToken);
            var rab = new RabbitMqOptions();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _busService.StopAsync(cancellationToken);
        }
    }
}
