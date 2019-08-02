using HomeSystem.Services.Identity.Domain.Repositories;
using HomeSystem.Services.Identity.Infrastructure.EF.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HomeSystem.Services.Identity.Infrastructure.EF.Extensions
{
    public static class Extensions
    {
        public static void AddEntityFramework(IServiceCollection services)
        {
            services.AddEntityFrameworkInMemoryDatabase()
                .AddEntityFrameworkSqlServer()
                .AddDbContext<IdentityDbContext>(ServiceLifetime.Scoped);

            services.AddScoped<IOneTimeSecuredOperationRepository, OneTimeSecuredOperationRepository>();        
            services.AddScoped<IUserRepository, UserRepository>();        
            services.AddScoped<IUserSessionRepository, UserSessionRepository>();        
        }
    }
}