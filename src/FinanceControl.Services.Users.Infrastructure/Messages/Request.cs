using System;
using FinanceControl.Services.Users.Domain.Extensions;

namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public class Request
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Origin { get; set; }
        public string Resource { get; set; }
        public string Culture { get; set; }
        private DateTime CreatedAt { get; set; }

        public static Request From<T>(Request request)
        {
            return Create<T>(request.Id, request.Origin, request.Culture, request.Resource);
        }

        public static Request New<T>()
        {
            return New<T>(Guid.NewGuid());
        }

        public static Request New<T>(Guid id)
        {
            return Create<T>(id, string.Empty, string.Empty, string.Empty);
        }

        public static Request Create<T>(Guid id, string origin, string culture, string resource)
        {
            return new Request
            {
                Id = id,
                Name = GetName(typeof(T).Name),
                Origin = origin.StartsWith("/") ? origin.Remove(0, 1) : origin,
                Culture = culture,
                Resource = resource,
                CreatedAt = DateTime.UtcNow
            };
        }

        private static string GetName(string name)
        {
            return name.Underscore().ToLowerInvariant();
        }
    }
}