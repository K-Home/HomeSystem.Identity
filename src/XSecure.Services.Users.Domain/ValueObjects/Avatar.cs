using System;
using XSecure.Services.Users.Domain.Extensions;
using XSecure.Services.Users.Domain.Types;

namespace XSecure.Services.Users.Domain.ValueObjects
{
    public class Avatar : ValueObject<Avatar>
    {
        public string Name { get; protected set; }
        public string Url { get; protected set; }

        public bool IsEmpty
        {
            get => Name.IsEmpty();
            set { } //Required by EF
        }

        protected Avatar()
        {
        }

        protected Avatar(string name, string url)
        {
            if (name.IsEmpty())
            {
                throw new ArgumentException("Avatar name can not be empty.", nameof(name));
            }

            if (url.IsEmpty())
            {
                throw new ArgumentException("Avatar Url can not be empty.", nameof(url));
            }

            Name = name;
            Url = url;
        }

        public static Avatar Empty => new Avatar();

        public static Avatar Create(string name, string url)
            => new Avatar(name, url);

        protected override bool EqualsCore(Avatar other) => Name.Equals(other.Name);

        protected override int GetHashCodeCore() => Name.GetHashCode();
    }
}