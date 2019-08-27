using Autofac;
using Autofac.Extensions.DependencyInjection;
using HomeSystem.Services.Identity.Application.Modules;
using HomeSystem.Services.Identity.Extensions;
using HomeSystem.Services.Identity.Infrastructure.Authorization.Extensions;
using HomeSystem.Services.Identity.Infrastructure.EF.Extensions;
using HomeSystem.Services.Identity.Infrastructure.Files.Modules;
using HomeSystem.Services.Identity.Infrastructure.MassTransit.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace HomeSystem.Services.Identity
{
    public class Startup
    {
        private static readonly string[] Headers = new[] { "X-Operation", "X-Resource", "X-Total-Count" };

        public IContainer Container { get; private set; }
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc();
            services.AddJwtAuth();
            services.AddEntityFramework();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", cors =>
                    cors.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithExposedHeaders(Headers));
            });

            var builder = new ContainerBuilder();

            builder.RegisterModule<FilesModule>();
            builder.RegisterModule<MapperModule>();
            builder.RegisterModule<ApplicationModule>();
            builder.RegisterModule<MediatRModule>();
            builder.RegisterModule<MassTransitModule>();
            builder.RegisterResourceModule();

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

            app.UseCors("CorsPolicy");
            app.UseErrorHandler();
            app.UseAllForwardedHeaders();
            app.UseMvc();

            applicationLifetime.ApplicationStopped.Register(() =>
            {
                Container.Dispose();
            });
        }
    }
}

