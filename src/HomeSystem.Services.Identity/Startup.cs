using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using HomeSystem.Services.Identity.DAL.Extensions;
using HomeSystem.Services.Identity.Settings;
using KShared.Authentication.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.Configure<SqlOptions>(Configuration.GetSection("sql"));
            services.AddEntityFramework();
            services.AddJwt();

            var builder = new ContainerBuilder();
            
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();
            
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

            app.UseAccessTokenValidator();
            app.UseAuthentication();
            app.UseMvc();
            
            applicationLifetime.ApplicationStopped.Register(() => 
            { 
                Container.Dispose(); 
            });
        }
    }
}

