using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace HomeSystem.Services.Identity.Infrastructure.Authorization.Extensions
{
    public static class AuthorizationModule
    {
        private const string SectionName = "jwt";

        public static void AddJwtAuth(this IServiceCollection services)
        {
            IConfiguration configuration;

            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            var settings = new JwtTokenSettings();
            services.Configure<JwtTokenSettings>(opt => configuration.GetSection(SectionName).Bind(settings));
            services.AddSingleton<IJwtTokenHandler, JwtTokenHandler>();
            services.AddAuthentication()
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey)),
                        ValidIssuer = settings.Issuer,
                        ValidateIssuer = settings.ValidateIssuer,
                        ValidAudience = settings.ValidAudience,
                        ValidateAudience = settings.ValidateAudience,
                        ValidateLifetime = settings.ValidateLifetime,
                    };
                });
        }
    }
}
