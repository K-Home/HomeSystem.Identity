using System;
using HomeSystem.Services.Identity.Domain.Extensions;
using HomeSystem.Services.Identity.Domain.Types;

namespace HomeSystem.Services.Identity.Domain.ValueObjects
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