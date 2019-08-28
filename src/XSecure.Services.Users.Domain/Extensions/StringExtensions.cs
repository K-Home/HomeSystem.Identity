using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace XSecure.Services.Users.Domain.Extensions
{
    public static class StringExtensions
    {
        private static readonly Regex EmailRegex = new Regex(
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

        private static readonly Regex PhoneNumberRegex = new Regex(
            @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$", 
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        private static readonly Regex NameRegex = new Regex("^(?![_.-])(?!.*[_.-]{2})[a-zA-Z0-9._.-]+(?<![_.-])$",
            RegexOptions.Compiled);

        public static bool IsEmpty(this string target) => string.IsNullOrWhiteSpace(target);

        public static bool IsNotEmpty(this string target) => !target.IsEmpty();

        public static string TrimToUpper(this string value)
        {
            return value.OrEmpty().Trim().ToUpperInvariant();
        }

        public static string TrimToLower(this string value)
        {
            return value.OrEmpty().Trim().ToLowerInvariant();
        }

        public static string OrEmpty(this string value)
        {
            return value.IsEmpty() ? "" : value;
        }

        public static string CutSpecificEndWord(this string value, string valueToRemove)
            => value.Replace(valueToRemove, "");

        public static bool EqualsCaseInvariant(this string value, string valueToCompare)
        {
            if (value.IsEmpty())
                return valueToCompare.IsEmpty();
            if (valueToCompare.IsEmpty())
                return false;

            var fixedValue = value.TrimToUpper();
            var fixedValueToCompare = valueToCompare.TrimToUpper();

            return fixedValue == fixedValueToCompare;
        }

        public static bool Like(this string value, string valueToCompare)
        {
            if (value.IsEmpty())
                return valueToCompare.IsEmpty();

            var fixedValue = value.TrimToUpper();
            var fixedValueToCompare = valueToCompare.TrimToUpper();

            return fixedValue.Contains(fixedValueToCompare);
        }

        public static string AggregateLines(this IEnumerable<string> values)
            => values.Aggregate((x, y) => $"{x.Trim()}\n{y.Trim()}");

        public static bool IsEmail(this string value) => value.IsNotEmpty() && EmailRegex.IsMatch(value);

        public static bool IsPhoneNumber(this string value) => value.IsNotEmpty() && PhoneNumberRegex.IsMatch(value);

        public static bool IsName(this string value) => value.IsNotEmpty() && NameRegex.IsMatch(value);

        public static string Underscore(this string value)
           => string.Concat(value.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()));
    }
}