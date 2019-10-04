using System;
using FinanceControl.Services.Users.Domain.Extensions;

namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public class Request
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Resource { get; set; }
        public string UserAgent { get; set; }
        public string IpAddress { get; set; }
        public string Culture { get; set; }
        private DateTime CreatedAt { get; set; }

        public static Request From<T>(Request request)
        {
            return Create<T>(request.Id, request.Culture, request.Resource, request.UserAgent, request.IpAddress);
        }

        public static Request New<T>()
        {
            return New<T>(Guid.NewGuid());
        }

        public static Request New<T>(Guid id)
        {
            return Create<T>(id, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        public static Request Create<T>(Guid id, string culture, string resource, string userAgent, string ipAddress)
        {
            return new Request
            {
                Id = id,
                Name = GetName(typeof(T).Name),
                Culture = culture,
                Resource = resource,
                IpAddress = ipAddress,
                UserAgent = userAgent,
                CreatedAt = DateTime.UtcNow
            };
        }

        private static string GetName(string name)
        {
            return name
                .Substring(name.Length - 6)
                .Underscore()
                .ToLowerInvariant();
        }
    }
}