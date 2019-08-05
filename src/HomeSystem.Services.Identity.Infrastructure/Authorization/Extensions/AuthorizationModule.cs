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
            var section = configuration.GetSection(SectionName);
            services.Configure<JwtTokenSettings>(section);
            section.Bind(settings);
            services.AddSingleton(settings);
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
