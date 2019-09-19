using System;

namespace FinanceControl.Services.Users.Domain.Extensions
{
    public static class NullSecurity
    {
        public static void ThrowIfNull<T>(this T value) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(typeof(T).Name);
            }
        }

        public static T CheckIfNotEmpty<T>(this T value) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(typeof(T).Name);
            }

            return value;
        }

        public static void ThrowIfNull<T>(this T value, string message) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(typeof(T).Name, message);
            }
        }

        public static bool HasNoValue<T>(this T value) where T : class
        {
            return value == null;
        }

        public static bool HasValue<T>(this T value) where T : class
        {
            return value != null;
        }
    }
}