using System;
using HomeSystem.Services.Identity.Exceptions;
using KShared.ClassExtensions;
using KShared.Domain.BaseClasses;
using KShared.Exceptions.Exceptions;

namespace HomeSystem.Services.Identity.Domain.Aggregates
{
    public class OneTimeSecuredOperation : Entity, ITimestampable
    {
        public string Type { get; private set; }
        public string User { get; private set; }
        public string Token { get; private set; }
        public string RequesterIpAddress { get; private set; }
        public string RequesterUserAgent { get; private set; }
        public string ConsumerIpAddress { get; private set; }
        public string ConsumerUserAgent { get; private set; }
        public bool Consumed => ConsumedAt.HasValue;
        public DateTime? ConsumedAt { get; private set; }
        public DateTime Expiry { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected OneTimeSecuredOperation()
        {
        }

        public OneTimeSecuredOperation(Guid id, string type,
            string user, string token, DateTime expiry,
            string ipAddress = null, string userAgent = null)
        {
            if (type.IsEmpty())
            {
                throw new DomainException(Codes.InvalidSecuredOperation,
                    "Type can not be empty.");
            }

            if (user.IsEmpty())
            {
                throw new DomainException(Codes.InvalidSecuredOperation,
                    "User can not be empty.");
            }

            if (token.IsEmpty())
            {
                throw new DomainException(Codes.InvalidSecuredOperation,
                    "Token can not be empty.");
            }

            Id = id;
            Type = type;
            User = user;
            Token = token;
            Expiry = expiry.ToUniversalTime();
            RequesterIpAddress = ipAddress;
            RequesterUserAgent = userAgent;
            CreatedAt = DateTime.UtcNow;
        }

        public void Consume(string ipAddress = null, string userAgent = null)
        {
            if (!CanBeConsumed())
            {
                throw new DomainException(Codes.InvalidSecuredOperation,
                    "Operation can not be consumed.");
            }

            ConsumerIpAddress = ipAddress;
            ConsumerUserAgent = userAgent;
            ConsumedAt = DateTime.UtcNow;
        }

        public bool CanBeConsumed()
        {
            if (Consumed)
                return false;

            return Expiry > DateTime.UtcNow;
        }
    }
}