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

        IHandlerTask OnCustomError(Action<FinanceControlException> actOnCustomError,
            bool propagateException = false, bool executeOnError = false);

        IHandlerTask OnCustomError(Action<FinanceControlException, ILogger> actOnCustomErrorWithLogger,
            bool propagateException = false, bool executeOnError = false);

        IHandlerTask OnCustomError(Func<FinanceControlException, Task> funcOnCustomError,
            bool propagateException = false, bool executeOnError = false);

        IHandlerTask OnCustomError(Func<FinanceControlException, ILogger, Task> funcOnCustomErrorWithLogger,
            bool propagateException = false, bool executeOnError = false);

        IHandlerTask OnError(Action<Exception> actOnError);
        IHandlerTask OnError(Action<Exception> actOnError, bool propagateException);
        IHandlerTask OnError(Action<Exception, ILogger> actOnErrorWithLogger);
        IHandlerTask OnError(Action<Exception, ILogger> actOnErrorWithLogger, bool propagateException);
        IHandlerTask OnError(Func<Exception, Task> funcOnError);
        IHandlerTask OnError(Func<Exception, Task> funcOnError, bool propagateException);
        IHandlerTask OnError(Func<Exception, ILogger, Task> funcOnErrorWithLogger);
        IHandlerTask OnError(Func<Exception, ILogger, Task> funcOnErrorWithLogger, bool propagateException);
        IHandlerTask OnSuccess(Action onSuccess);
        IHandlerTask OnSuccess(Func<Task> onSuccess);
        IHandlerTask PropagateException();
        IHandlerTask DoNotPropagateException();
        IHandler Next();
        void Execute();
        Task ExecuteAsync();
    }
}