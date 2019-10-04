using System.Linq;
using FinanceControl.Services.Users.Domain.Extensions;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Microsoft.AspNetCore.Http;

namespace FinanceControl.Services.Users.Api.Extensions
{
    public static class RequestExtensions
    {
        public static void BindRequest<TCommand>(this TCommand command, HttpContext httpContext)
            where TCommand : class, ICommand
        {
            command.Request.Culture = GetCultureFromHeader(httpContext);
            command.Request.Resource = GetResourceFromHeader(httpContext);
            command.Request.UserAgent = GetUserAgent(httpContext);
            command.Request.IpAddress = GetIpAddressFromHeader(httpContext);
        }

        public static string GetResourceFromHeader(HttpContext httpContext)
        {
            var resource = httpContext.Request.Path.ToString();
            var formattedResourceString = resource.StartsWith("/") ? resource.Remove(0, 1) : resource;

            return resource.IsEmpty() ? string.Empty : formattedResourceString;
        }

        public static string GetCultureFromHeader(HttpContext httpContext)
        {
            var userLanguages = httpContext.Request.Headers["Accept-Language"].ToString();
            var firstLang = userLanguages.Split(',').FirstOrDefault();
            var culture = firstLang.IsEmpty() ? "en" : firstLang;

            return culture;
        }

        public static string GetIpAddressFromHeader(HttpContext httpContext)
        {
            var ipAddress = httpContext.Connection.RemoteIpAddress.ToString();

            return ipAddress.IsEmpty() ? string.Empty : ipAddress;
        }

        public static string GetUserAgent(HttpContext httpContext)
        {
            var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
            var userAgentString = userAgent.IsEmpty() ? string.Empty : userAgent;

            return userAgentString;
        }
    }
}