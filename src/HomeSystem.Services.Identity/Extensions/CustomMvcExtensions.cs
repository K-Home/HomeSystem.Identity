using HomeSystem.Services.Identity.Framework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace HomeSystem.Services.Identity.Extensions
{
    public static class CustomMvcExtensions
    {
        public static IMvcCoreBuilder AddCustomMvc(this IServiceCollection services)
        {
            return services
                .AddMvcCore()
                .AddJsonFormatters()
                .AddDataAnnotations()
                .AddApiExplorer()
                .AddDefaultJsonOptions()
                .AddAuthorization();
        }

        public static IMvcCoreBuilder AddDefaultJsonOptions(this IMvcCoreBuilder builder)
            => builder.AddJsonOptions(o =>
            {
                o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                o.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                o.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
                o.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                o.SerializerSettings.Formatting = Formatting.Indented;
                o.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

        public static IApplicationBuilder UseAllForwardedHeaders(this IApplicationBuilder builder)
            => builder.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware<ErrorHandlerMiddleware>();
    }
}