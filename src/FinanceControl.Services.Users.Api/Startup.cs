using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FinanceControl.Services.Users.Api.Extensions;
using FinanceControl.Services.Users.Application.Modules;
using FinanceControl.Services.Users.Infrastructure.Authorization.Extensions;
using FinanceControl.Services.Users.Infrastructure.EF.Extensions;
using FinanceControl.Services.Users.Infrastructure.Files.Modules;
using FinanceControl.Services.Users.Infrastructure.MassTransit.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceControl.Services.Users.Api
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

