using Serilog;
using System;
using System.Threading.Tasks;
using XSecure.Services.Users.Domain.Exceptions;

namespace XSecure.Services.Users.Infrastructure.Handlers
{
    public interface IHandlerTask
    {
        IHandlerTask Always(Action always);
        IHandlerTask Always(Func<Task> always);

        IHandlerTask OnCustomError(Action<XSecureException> onCustomError,
            bool propagateException = false, bool executeOnError = false);

        IHandlerTask OnCustomError(Action<XSecureException, ILogger> onCustomError,
            bool propagateException = false, bool executeOnError = false);

        IHandlerTask OnCustomError(Func<XSecureException, Task> onCustomError,
            bool propagateException = false, bool executeOnError = false);

        IHandlerTask OnCustomError(Func<XSecureException, ILogger, Task> onCustomError,
            bool propagateException = false, bool executeOnError = false);

        IHandlerTask OnError(Action<Exception> onError, bool propagateException = false);
        IHandlerTask OnError(Action<Exception, ILogger> onError, bool propagateException = false);
        IHandlerTask OnError(Func<Exception, Task> onError, bool propagateException = false);
        IHandlerTask OnError(Func<Exception, ILogger, Task> onError, bool propagateException = false);
        IHandlerTask OnSuccess(Action onSuccess);
        IHandlerTask OnSuccess(Func<Task> onSuccess);
        IHandlerTask PropagateException();
        IHandlerTask DoNotPropagateException();
        IHandler Next();
        void Execute();
        Task ExecuteAsync();
    }
}