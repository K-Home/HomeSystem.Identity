using System;
using KShared.ClassExtensions;
using KShared.Domain.BaseClasses;

namespace HomeSystem.Services.Identity.Domain.ValueObjects
{
    public class UserAddress : ValueObject<UserAddress>
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }

        protected UserAddress()
        {
        }

        public UserAddress(string street, string city, string state, string country, string zipCode)
        {
            if (street.IsEmpty())
            {
                throw new ArgumentException("Street can not be null or empty.", nameof(street));
            }

            if (city.IsEmpty())
            {
                throw new ArgumentException("City can not be null or empty.", nameof(city));
            }

            if (state.IsEmpty())
            {
                throw new ArgumentException("State can not be null or empty.", nameof(state));
            }

            if (country.IsEmpty())
            {
                throw new ArgumentException("Country can not be null or empty.", nameof(country));
            }

            if (zipCode.IsEmpty())
            {
                throw new ArgumentException("Zip code can not be null or empty.", nameof(zipCode));
            }

            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }

        public static UserAddress Create(string street, string city, string state, string country, string zipCode)
            => new UserAddress(street, city, state, country, zipCode);

        protected override bool EqualsCore(UserAddress other)
            => Street.Equals(other.Street) && City.Equals(other.City) && State.Equals(other.State) &&
               Country.Equals(other.Country) && ZipCode.Equals(other.ZipCode);

        protected override int GetHashCodeCore()
        {
            var hash = 13;
            hash = (hash * 7) + Street.GetHashCode();
            hash = (hash * 7) + City.GetHashCode();
            hash = (hash * 7) + State.GetHashCode();
            hash = (hash * 7) + Country.GetHashCode();
            hash = (hash * 7) + ZipCode.GetHashCode();

            return hash;
        }
    }
}