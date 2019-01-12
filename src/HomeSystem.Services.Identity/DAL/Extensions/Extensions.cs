using Microsoft.Extensions.DependencyInjection;

namespace HomeSystem.Services.Identity.DAL.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddEntityFramework(this IServiceCollection services)
            => services.AddEntityFrameworkInMemoryDatabase()
                .AddEntityFrameworkSqlServer()
                .AddDbContext<IdentityDbContext>();
    }
}