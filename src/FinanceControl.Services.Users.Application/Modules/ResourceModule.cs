using System;
using System.Collections.Generic;
using Autofac;
using FinanceControl.Services.Users.Application.Messages.DomainEvents;
using FinanceControl.Services.Users.Application.Services;
using FinanceControl.Services.Users.Application.Services.Base;
using FinanceControl.Services.Users.Domain.Extensions;
using FinanceControl.Services.Users.Infrastructure;
using FinanceControl.Services.Users.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;

namespace FinanceControl.Services.Users.Application.Modules
{
    public static class ResourceModuleRegistration
    {
        public static void RegisterResourceModule(this ContainerBuilder builder)
        {
            var resources = new Dictionary<Type, string>
            {
                [typeof(SignedUpDomainEvent)] = "users/accounts/{0}"
            };

            builder.RegisterModule(new ResourceModule(resources));
        }
    }

    public class ResourceModule : Module
    {
        private readonly IDictionary<Type, string> _resources;

        public ResourceModule(IDictionary<Type, string> resources)
        {
            _resources = resources.CheckIfNotEmpty();
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var options = configuration.GetOptions<AppOptions>("app");

                return options;
            }).SingleInstance();

            builder.Register((c, p) =>
                {
                    var settings = c.Resolve<AppOptions>();

                    return new ResourceService(settings.ServiceName, _resources);
                })
                .As<IResourceService>()
                .SingleInstance();
        }
    }
}