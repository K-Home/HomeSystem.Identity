using System;

namespace XSecure.Services.Users.Domain.Exceptions
{
    public class XSecureException : Exception
    {
        public string Code { get; }

        protected XSecureException()
        {
        }

        protected XSecureException(string code)
        {
            Code = code;
        }

        protected XSecureException(string message, params object[] args) : this(string.Empty, message, args)
        {
        }

        protected XSecureException(string code, string message, params object[] args) : this(null, code, message,
            args)
        {
        }

        protected XSecureException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        protected XSecureException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}