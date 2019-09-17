using System;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Domain.Exceptions;
using Serilog;

namespace FinanceControl.Services.Users.Infrastructure.Handlers
{
    public interface IHandlerTask
    {
        IHandlerTask Always(Action always);
        IHandlerTask Always(Func<Task> always);

        IHandlerTask OnCustomError(Action<FinanceControlException> onCustomError,
            bool propagateException = false, bool executeOnError = false);

        IHandlerTask OnCustomError(Action<FinanceControlException, ILogger> onCustomError,
            bool propagateException = false, bool executeOnError = false);

        IHandlerTask OnCustomError(Func<FinanceControlException, Task> onCustomError,
            bool propagateException = false, bool executeOnError = false);

        IHandlerTask OnCustomError(Func<FinanceControlException, ILogger, Task> onCustomError,
            bool propagateException = false, bool executeOnError = false);

        IHandlerTask OnError(Action<Exception> onError);
        IHandlerTask OnError(Action<Exception> onError, bool propagateException);
        IHandlerTask OnError(Action<Exception, ILogger> onError);
        IHandlerTask OnError(Action<Exception, ILogger> onError, bool propagateException);
        IHandlerTask OnError(Func<Exception, Task> onError);
        IHandlerTask OnError(Func<Exception, Task> onError, bool propagateException);
        IHandlerTask OnError(Func<Exception, ILogger, Task> onError);
        IHandlerTask OnError(Func<Exception, ILogger, Task> onError, bool propagateException);
        IHandlerTask OnSuccess(Action onSuccess);
        IHandlerTask OnSuccess(Func<Task> onSuccess);
        IHandlerTask PropagateException();
        IHandlerTask DoNotPropagateException();
        IHandler Next();
        void Execute();
        Task ExecuteAsync();
    }
}