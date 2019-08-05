using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using HomeSystem.Services.Identity.Application.Modules;
using HomeSystem.Services.Identity.Infrastructure.Authorization.Extensions;
using HomeSystem.Services.Identity.Infrastructure.Files.Modules;
using static HomeSystem.Services.Identity.Infrastructure.EF.Extensions.EntityFrameworkModule;

namespace HomeSystem.Services.Identity
{
    public class Startup
    {
        public IContainer Container { get; private set; }
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddJwtAuth();
            services.AddEntityFramework();

            var builder = new ContainerBuilder();

            builder.RegisterModule<FilesModule>();
            builder.RegisterModule<MapperModule>();
            builder.RegisterModule<ApplicationModule>();
            builder.RegisterModule<MediatRModule>();
            builder.Populate(services);

            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment() || env.EnvironmentName == "local")
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            
            applicationLifetime.ApplicationStopped.Register(() => 
            { 
                Container.Dispose(); 
            });
        }
    }
}

