using System;

namespace FinanceControl.Services.Users.Domain.Exceptions
{
    public class FinanceControlException : Exception
    {
        public string Code { get; }

        protected FinanceControlException()
        {
        }

        protected FinanceControlException(string code)
        {
            Code = code;
        }

        protected FinanceControlException(string message, params object[] args) : this(string.Empty, message, args)
        {
        }

        protected FinanceControlException(string code, string message, params object[] args) : this(null, code, message,
            args)
        {
        }

        protected FinanceControlException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        protected FinanceControlException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}