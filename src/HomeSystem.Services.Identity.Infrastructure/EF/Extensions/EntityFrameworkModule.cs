using System;
using System.Reflection;
using HomeSystem.Services.Identity.Domain.Repositories;
using HomeSystem.Services.Identity.Infrastructure.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HomeSystem.Services.Identity.Infrastructure.EF.Extensions
{
    public static class EntityFrameworkModule
    {
        private const string SectionName = "sql";

        public static void AddEntityFramework(this IServiceCollection services)
        {
            IConfiguration configuration;

            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            services.AddScoped<IOneTimeSecuredOperationRepository, OneTimeSecuredOperationRepository>();        
            services.AddScoped<IUserRepository, UserRepository>();        
            services.AddScoped<IUserSessionRepository, UserSessionRepository>();

            var settings = new SqlOptions();
            services.Configure<SqlOptions>(opt => configuration.GetSection(SectionName).Bind(settings));

            services.AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<IdentityDbContext>(opt =>
                {
                    opt.UseSqlServer(settings.ConnectionString,
                        sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(IdentityDbContext).GetTypeInfo().Assembly.GetName().Name);
                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                        });
                });
        }
    }
}