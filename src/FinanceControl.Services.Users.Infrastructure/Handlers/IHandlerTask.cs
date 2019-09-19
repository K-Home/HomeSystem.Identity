using System;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Domain.Exceptions;
using Serilog;

namespace FinanceControl.Services.Users.Infrastructure.Handlers
{
    public interface IHandlerTask
    {
        IHandlerTask Always(Action alwaysAction);
        IHandlerTask Always(Func<Task> alwaysAsyncAction);

        IHandlerTask OnCustomError(Action<FinanceControlException> onCustomErrorAction,
            bool propagateException = false, bool executeOnError = false);

        IHandlerTask OnCustomError(Action<FinanceControlException, ILogger> onCustomErrorAsyncWithLoggerAction,
            bool propagateException = false, bool executeOnError = false);

        IHandlerTask OnCustomError(Func<FinanceControlException, Task> onCustomErrorAsyncAction,
            bool propagateException = false, bool executeOnError = false);

        IHandlerTask OnCustomError(Func<FinanceControlException, ILogger, Task> onCustomErrorAsyncWithLoggerAction,
            bool propagateException = false, bool executeOnError = false);

        IHandlerTask OnError(Action<Exception> onErrorAction);
        IHandlerTask OnError(Action<Exception> onErrorAction, bool propagateException);
        IHandlerTask OnError(Action<Exception, ILogger> onErrorWithLoggerAction);
        IHandlerTask OnError(Action<Exception, ILogger> onErrorWithLoggerAction, bool propagateException);
        IHandlerTask OnError(Func<Exception, Task> onErrorAsyncAction);
        IHandlerTask OnError(Func<Exception, Task> onErrorAsyncAction, bool propagateException);
        IHandlerTask OnError(Func<Exception, ILogger, Task> onErrorAsyncWithLoggerAction);
        IHandlerTask OnError(Func<Exception, ILogger, Task> onErrorAsyncWithLoggerAction, bool propagateException);
        IHandlerTask OnSuccess(Action onSuccessAction);
        IHandlerTask OnSuccess(Func<Task> onSuccessAsyncAction);
        IHandlerTask PropagateException();
        IHandlerTask DoNotPropagateException();
        IHandler Next();
        void Execute();
        Task ExecuteAsync();
    }
}