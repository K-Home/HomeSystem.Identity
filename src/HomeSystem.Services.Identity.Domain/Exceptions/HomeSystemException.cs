using System;

namespace HomeSystem.Services.Identity.Domain.Exceptions
{
    public class HomeSystemException : Exception
    {
        public string Code { get; }

        protected HomeSystemException()
        {
        }

        protected HomeSystemException(string code)
        {
            Code = code;
        }

        protected HomeSystemException(string message, params object[] args) : this(string.Empty, message, args)
        {
        }

        protected HomeSystemException(string code, string message, params object[] args) : this(null, code, message,
            args)
        {
        }

        protected HomeSystemException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        protected HomeSystemException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}