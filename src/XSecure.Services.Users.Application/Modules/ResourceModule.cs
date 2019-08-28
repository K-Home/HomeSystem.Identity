using Autofac;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using XSecure.Services.Users.Application.Messages.DomainEvents;
using XSecure.Services.Users.Application.Services;
using XSecure.Services.Users.Application.Services.Base;
using XSecure.Services.Users.Infrastructure;
using XSecure.Services.Users.Infrastructure.Extensions;

namespace XSecure.Services.Users.Application.Modules
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
            _resources = resources;
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
    
