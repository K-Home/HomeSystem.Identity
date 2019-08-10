using HomeSystem.Services.Identity.Domain.Repositories;
using HomeSystem.Services.Identity.Infrastructure.EF.Repositories;
using HomeSystem.Services.Identity.Infrastructure.Extensions;
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

            var section = configuration.GetSection(SectionName);
            var settings = configuration.GetOptions<SqlOptions>(SectionName);
            services.Configure<SqlOptions>(section);
            services.AddSingleton(settings);

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<IdentityDbContext>();

            services.AddScoped<IOneTimeSecuredOperationRepository, OneTimeSecuredOperationRepository>();        
            services.AddScoped<IUserRepository, UserRepository>();        
            services.AddScoped<IUserSessionRepository, UserSessionRepository>();
        }
    }
}