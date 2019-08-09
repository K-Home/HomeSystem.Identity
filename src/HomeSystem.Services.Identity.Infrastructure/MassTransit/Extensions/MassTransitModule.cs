using Autofac;
using GreenPipes;
using HomeSystem.Services.Identity.Infrastructure.MassTransit.Options;
using MassTransit;
using System;
using HomeSystem.Services.Identity.Infrastructure.Extensions;
using HomeSystem.Services.Identity.Infrastructure.MassTransit.MassTransitBus;
using Microsoft.Extensions.Configuration;

namespace HomeSystem.Services.Identity.Infrastructure.MassTransit.Extensions
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

