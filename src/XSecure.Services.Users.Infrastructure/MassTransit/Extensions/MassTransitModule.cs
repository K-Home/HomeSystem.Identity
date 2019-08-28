using Autofac;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Configuration;
using XSecure.Services.Users.Infrastructure.Extensions;
using XSecure.Services.Users.Infrastructure.MassTransit.MassTransitBus;
using XSecure.Services.Users.Infrastructure.MassTransit.Options;

namespace XSecure.Services.Users.Infrastructure.MassTransit.Extensions
{
    public class MassTransitModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var options = configuration.GetOptions<RabbitMqOptions>("rabbitMq");

                return options;
            }).SingleInstance();

            builder.Register(context =>
            {
                return Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    var rabbitMqOptions = context.Resolve<RabbitMqOptions>();

                    var host = config.Host(rabbitMqOptions.HostName, rabbitMqOptions.Port, 
                        rabbitMqOptions.VirtualHost, h =>
                    {
                        h.Username(rabbitMqOptions.Username);
                        h.Password(rabbitMqOptions.Password);
                    });

                    config.ReceiveEndpoint(host, rabbitMqOptions.QueueName, e =>
                    {
                        e.PrefetchCount = rabbitMqOptions.PrefetchCount;
                        e.UseMessageRetry(mr => mr.Interval(rabbitMqOptions.RetryIntervalMinValue,
                            rabbitMqOptions.RetryIntervalMaxValue));
                    });
                });
            })
            .SingleInstance()
            .As<IBusControl>()
            .As<IBus>();

            builder.RegisterType<MassTransitBusService>().As<IMassTransitBusService>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}

