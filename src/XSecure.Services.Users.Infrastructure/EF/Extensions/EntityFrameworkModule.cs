using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XSecure.Services.Users.Domain.Repositories;
using XSecure.Services.Users.Infrastructure.EF.Repositories;
using XSecure.Services.Users.Infrastructure.Extensions;

namespace XSecure.Services.Users.Infrastructure.EF.Extensions
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