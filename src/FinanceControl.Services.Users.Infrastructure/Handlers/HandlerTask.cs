using System;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Domain.Exceptions;
using FinanceControl.Services.Users.Domain.Extensions;
using Serilog;

namespace FinanceControl.Services.Users.Infrastructure.Handlers
{
    public class HandlerTask : IHandlerTask
    {
        private static readonly ILogger Logger = Log.Logger;

        private readonly IHandler _handler;
        private readonly Action _run;
        private readonly Func<Task> _runAsync;
        private readonly Action _validate;
        private readonly Func<Task> _validateAsync;

        private Action _always;
        private Func<Task> _alwaysAsync;
        private Action _onSuccess;
        private Func<Task> _onSuccessAsync;
        private Action<Exception> _onError;
        private Action<Exception, ILogger> _onErrorWithLogger;
        private Action<FinanceControlException> _onCustomError;
        private Action<FinanceControlException, ILogger> _onCustomErrorWithLogger;
        private Func<Exception, Task> _onErrorAsync;
        private Func<Exception, ILogger, Task> _onErrorWithLoggerAsync;
        private Func<FinanceControlException, Task> _onCustomErrorAsync;
        private Func<FinanceControlException, ILogger, Task> _onCustomErrorWithLoggerAsync;
        private bool _propagateException = true;
        private bool _executeOnError = true;

        public HandlerTask(IHandler handler, Action runAction)
        {
            _handler = handler.CheckIfNotEmpty();
            _run = runAction.CheckIfNotEmpty();
        }

        public HandlerTask(IHandler handler, Func<Task> runAsyncAction)
        {
            _handler = handler.CheckIfNotEmpty();
            _runAsync = runAsyncAction.CheckIfNotEmpty();
        }

        public HandlerTask(IHandler handler, Action runAction,
            Action validate)
        {
            _handler = handler.CheckIfNotEmpty();
            _run = runAction.CheckIfNotEmpty();
            _validate = validate.CheckIfNotEmpty();
        }

        public HandlerTask(IHandler handler, Action runAction,
            Func<Task> validateActionAsync)
        {
            _handler = handler.CheckIfNotEmpty();
            _run = runAction.CheckIfNotEmpty();
            _validateAsync = validateActionAsync.CheckIfNotEmpty();
        }

        public HandlerTask(IHandler handler, Action runAction,
            Action validateAction, Func<Task> validateAsyncAction)
        {
            _handler = handler.CheckIfNotEmpty();
            _run = runAction.CheckIfNotEmpty();
            _validate = validateAction.CheckIfNotEmpty();
            _validateAsync = validateAsyncAction.CheckIfNotEmpty();
        }

        public HandlerTask(IHandler handler, Func<Task> runAsyncAsync,
            Action validateAction)
        {
            _handler = handler.CheckIfNotEmpty();
            _runAsync = runAsyncAsync.CheckIfNotEmpty();
            _validate = validateAction.CheckIfNotEmpty();
        }

        public HandlerTask(IHandler handler, Func<Task> runAsyncAsync,
            Func<Task> validateAsyncAction)
        {
            _handler = handler.CheckIfNotEmpty();
            _runAsync = runAsyncAsync.CheckIfNotEmpty();
            _validateAsync = validateAsyncAction.CheckIfNotEmpty();
        }

        public HandlerTask(IHandler handler, Func<Task> runAsyncAsync,
            Action validateAction, Func<Task> validateAsyncAction)
        {
            _handler = handler.CheckIfNotEmpty();
            _runAsync = runAsyncAsync.CheckIfNotEmpty();
            _validate = validateAction.CheckIfNotEmpty();
            _validateAsync = validateAsyncAction.CheckIfNotEmpty();
        }

        public IHandlerTask Always(Action alwaysAction)
        {
            _always = alwaysAction;

            return this;
        }

        public IHandlerTask Always(Func<Task> alwaysAsyncAction)
        {
            _alwaysAsync = alwaysAsyncAction;

            return this;
        }

        public IHandlerTask OnCustomError(Action<FinanceControlException> onCustomErrorAction,
            bool propagateException = false, bool executeOnError = false)
        {
            _onCustomError = onCustomErrorAction;
            _propagateException = propagateException;
            _executeOnError = executeOnError;

            return this;
        }

        public IHandlerTask OnCustomError(Action<FinanceControlException, ILogger> onCustomErrorAsyncWithLoggerAction,
            bool propagateException = false, bool executeOnError = false)
        {
            _onCustomErrorWithLogger = onCustomErrorAsyncWithLoggerAction;
            _propagateException = propagateException;
            _executeOnError = executeOnError;

            return this;
        }

        public IHandlerTask OnCustomError(Func<FinanceControlException, Task> onCustomErrorAsyncAction,
            bool propagateException = false, bool executeOnError = false)
        {
            _onCustomErrorAsync = onCustomErrorAsyncAction;
            _propagateException = propagateException;
            _executeOnError = executeOnError;

            return this;
        }

        public IHandlerTask OnCustomError(
            Func<FinanceControlException, ILogger, Task> onCustomErrorAsyncWithLoggerAction,
            bool propagateException = false, bool executeOnError = false)
        {
            _onCustomErrorWithLoggerAsync = onCustomErrorAsyncWithLoggerAction;
            _propagateException = propagateException;
            _executeOnError = executeOnError;

            return this;
        }

        public IHandlerTask OnError(Action<Exception> onErrorAction)
        {
            _onError = onErrorAction;

            return this;
        }

        public IHandlerTask OnError(Action<Exception> onErrorAction, bool propagateException)
        {
            _onError = onErrorAction;
            _propagateException = propagateException;

            return this;
        }

        public IHandlerTask OnError(Action<Exception, ILogger> onErrorWithLoggerAction)
        {
            _onErrorWithLogger = onErrorWithLoggerAction;

            return this;
        }

        public IHandlerTask OnError(Action<Exception, ILogger> onErrorWithLoggerAction, bool propagateException)
        {
            _onErrorWithLogger = onErrorWithLoggerAction;
            _propagateException = propagateException;

            return this;
        }

        public IHandlerTask OnError(Func<Exception, Task> onErrorAsyncAction)
        {
            _onErrorAsync = onErrorAsyncAction;

            return this;
        }

        public IHandlerTask OnError(Func<Exception, Task> onErrorAsyncAction, bool propagateException)
        {
            _onErrorAsync = onErrorAsyncAction;
            _propagateException = propagateException;

            return this;
        }

        public IHandlerTask OnError(Func<Exception, ILogger, Task> onErrorAsyncWithLoggerAction)
        {
            _onErrorWithLoggerAsync = onErrorAsyncWithLoggerAction;

            return this;
        }

        public IHandlerTask OnError(Func<Exception, ILogger, Task> onErrorAsyncWithLoggerAction,
            bool propagateException)
        {
            _onErrorWithLoggerAsync = onErrorAsyncWithLoggerAction;
            _propagateException = propagateException;

            return this;
        }

        public IHandlerTask OnSuccess(Action onSuccessAction)
        {
            _onSuccess = onSuccessAction;

            return this;
        }

        public IHandlerTask OnSuccess(Func<Task> onSuccessAsyncAction)
        {
            _onSuccessAsync = onSuccessAsyncAction;

            return this;
        }

        public IHandlerTask PropagateException()
        {
            _propagateException = true;

            return this;
        }

        public IHandlerTask DoNotPropagateException()
        {
            _propagateException = false;

            return this;
        }

        public IHandler Next()
        {
            return _handler;
        }

        public void Execute()
        {
            try
            {
                _validate?.Invoke();
                _run();
                _onSuccess?.Invoke();
            }
            catch (Exception exception)
            {
                var customException = exception as FinanceControlException;
                if (customException.HasValue())
                {
                    _onCustomErrorWithLogger?.Invoke(customException, Logger);
                    _onCustomError?.Invoke(customException);
                }

                var executeOnError = _executeOnError || customException.HasNoValue();
                if (executeOnError)
                {
                    _onErrorWithLogger?.Invoke(customException, Logger);
                    _onError?.Invoke(exception);
                }

                if (_propagateException)
                {
                    throw;
                }
            }
            finally
            {
                _always?.Invoke();
            }
        }

        public async Task ExecuteAsync()
        {
            try
            {
                _validate?.Invoke();
                if (_validateAsync.HasValue())
                {
                    await _validateAsync();
                }

                await _runAsync();
                if (_onSuccessAsync.HasValue())
                {
                    await _onSuccessAsync();
                }
            }
            catch (Exception exception)
            {
                var customException = exception as FinanceControlException;
                if (customException.HasValue())
                {
                    _onCustomErrorWithLogger?.Invoke(customException, Logger);
                    if (_onCustomErrorWithLoggerAsync.HasValue())
                    {
                        await _onCustomErrorWithLoggerAsync(customException, Logger);
                    }

                    _onCustomError?.Invoke(customException);
                    if (_onCustomErrorAsync.HasValue())
                    {
                        await _onCustomErrorAsync(customException);
                    }
                }

                var executeOnError = _executeOnError || customException.HasNoValue();
                if (executeOnError)
                {
                    _onErrorWithLogger?.Invoke(customException, Logger);
                    if (_onErrorWithLoggerAsync.HasValue())
                    {
                        await _onErrorWithLoggerAsync(exception, Logger);
                    }

                    _onError?.Invoke(exception);
                    if (_onErrorAsync.HasValue())
                    {
                        await _onErrorAsync(exception);
                    }
                }

                if (_propagateException)
                {
                    throw;
                }
            }
            finally
            {
                if (_alwaysAsync.HasValue())
                {
                    await _alwaysAsync();
                }
            }
        }
    }
}