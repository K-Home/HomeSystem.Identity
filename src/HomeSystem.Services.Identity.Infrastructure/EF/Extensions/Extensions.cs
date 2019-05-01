using Microsoft.Extensions.DependencyInjection;

namespace HomeSystem.Services.Identity.Infrastructure.EF.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddIdentityDbContext(this IServiceCollection services)
            => services.AddEntityFrameworkInMemoryDatabase()
                .AddEntityFrameworkSqlServer()
                .AddDbContext<IdentityDbContext>();
    }
}