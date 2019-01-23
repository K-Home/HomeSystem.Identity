using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using HomeSystem.Services.Identity.DAL.Extensions;
using HomeSystem.Services.Identity.Domain.Aggregates;
using HomeSystem.Services.Identity.Settings;
using KShared.Authentication.Extensions;
using KShared.Configuration.Extensions;
using KShared.CQRS.Extensions;
using KShared.Redis.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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
            services.AddCustomMvc();
            services.Configure<SqlOptions>(Configuration.GetSection("sql"));
            services.AddEntityFramework();
            services.AddJwt();
            services.AddRedis();

            var builder = new ContainerBuilder();   
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();      
            
            builder.Populate(services);          
            builder.RegisterType<PasswordHasher<User>>()
                .As<IPasswordHasher<User>>();
            
            builder.AddCQRS();         
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
            app.UseAccessTokenValidator();
            app.UseMvc();
            
            applicationLifetime.ApplicationStopped.Register(() => 
            { 
                Container.Dispose(); 
            });
        }
    }
}

