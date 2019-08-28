using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using XSecure.Services.Users.Api.Extensions;
using XSecure.Services.Users.Application.Modules;
using XSecure.Services.Users.Infrastructure.Authorization.Extensions;
using XSecure.Services.Users.Infrastructure.EF.Extensions;
using XSecure.Services.Users.Infrastructure.Files.Modules;
using XSecure.Services.Users.Infrastructure.MassTransit.Extensions;

namespace XSecure.Services.Users.Api
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

