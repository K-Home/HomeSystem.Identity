using System;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Domain.Exceptions;
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

        public HandlerTask(IHandler handler, Action run)
        {
            _handler = handler;
            _run = run;
        }

        public HandlerTask(IHandler handler, Func<Task> runAsync)
        {
            _handler = handler;
            _runAsync = runAsync;
        }

        public HandlerTask(IHandler handler, Action run,
            Action validate)
        {
            _handler = handler;
            _run = run;
            _validate = validate;
        }
        
        public HandlerTask(IHandler handler, Action run,
            Func<Task> validateAsync)
        {
            _handler = handler;
            _run = run;
            _validateAsync = validateAsync;
        }
        
        public HandlerTask(IHandler handler, Action run,
            Action validate, Func<Task> validateAsync)
        {
            _handler = handler;
            _run = run;
            _validate = validate;
            _validateAsync = validateAsync;
        }

        public HandlerTask(IHandler handler, Func<Task> runAsync,
            Action validate)
        {
            _handler = handler;
            _runAsync = runAsync;
            _validate = validate;
        }
        
        public HandlerTask(IHandler handler, Func<Task> runAsync,
            Func<Task> validateAsync)
        {
            _handler = handler;
            _runAsync = runAsync;
            _validateAsync = validateAsync;
        }
        
        public HandlerTask(IHandler handler, Func<Task> runAsync,
            Action validate, Func<Task> validateAsync)
        {
            _handler = handler;
            _runAsync = runAsync;
            _validate = validate;
            _validateAsync = validateAsync;
        }

        public IHandlerTask Always(Action always)
        {
            _always = always;

            return this;
        }

        public IHandlerTask Always(Func<Task> always)
        {
            _alwaysAsync = always;

            return this;
        }

        public IHandlerTask OnCustomError(Action<FinanceControlException> onCustomError,
            bool propagateException = false, bool executeOnError = false)
        {
            _onCustomError = onCustomError;
            _propagateException = propagateException;
            _executeOnError = executeOnError;

            return this;
        }

        public IHandlerTask OnCustomError(Action<FinanceControlException, ILogger> onCustomError,
            bool propagateException = false, bool executeOnError = false)
        {
            _onCustomErrorWithLogger = onCustomError;
            _propagateException = propagateException;
            _executeOnError = executeOnError;

            return this;
        }

        public IHandlerTask OnCustomError(Func<FinanceControlException, Task> onCustomError,
            bool propagateException = false, bool executeOnError = false)
        {
            _onCustomErrorAsync = onCustomError;
            _propagateException = propagateException;
            _executeOnError = executeOnError;

            return this;
        }

        public IHandlerTask OnCustomError(Func<FinanceControlException, ILogger, Task> onCustomError,
            bool propagateException = false, bool executeOnError = false)
        {
            _onCustomErrorWithLoggerAsync = onCustomError;
            _propagateException = propagateException;
            _executeOnError = executeOnError;

            return this;
        }

        public IHandlerTask OnError(Action<Exception> onError)
        {
            _onError = onError;

            return this;
        }

        public IHandlerTask OnError(Action<Exception> onError, bool propagateException)
        {
            _onError = onError;
            _propagateException = propagateException;

            return this;
        }

        public IHandlerTask OnError(Action<Exception, ILogger> onError)
        {
            _onErrorWithLogger = onError;
            
            return this;
        }

        public IHandlerTask OnError(Action<Exception, ILogger> onError, bool propagateException)
        {
            _onErrorWithLogger = onError;
            _propagateException = propagateException;

            return this;
        }

        public IHandlerTask OnError(Func<Exception, Task> onError)
        {
            _onErrorAsync = onError;

            return this;
        }

        public IHandlerTask OnError(Func<Exception, Task> onError, bool propagateException)
        {
            _onErrorAsync = onError;
            _propagateException = propagateException;

            return this;
        }

        public IHandlerTask OnError(Func<Exception, ILogger, Task> onError)
        {
            _onErrorWithLoggerAsync = onError;
            
            return this;
        }

        public IHandlerTask OnError(Func<Exception, ILogger, Task> onError, bool propagateException)
        {
            _onErrorWithLoggerAsync = onError;
            _propagateException = propagateException;

            return this;
        }

        public IHandlerTask OnSuccess(Action onSuccess)
        {
            _onSuccess = onSuccess;

            return this;
        }

        public IHandlerTask OnSuccess(Func<Task> onSuccess)
        {
            _onSuccessAsync = onSuccess;

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
                if (customException != null)
                {
                    _onCustomErrorWithLogger?.Invoke(customException, Logger);
                    _onCustomError?.Invoke(customException);
                }

                var executeOnError = _executeOnError || customException == null;
                if (executeOnError)
                {
                    _onErrorWithLogger?.Invoke(customException, Logger);
                    _onError?.Invoke(exception);
                }

                if (_propagateException) throw;
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
                if (_validateAsync != null) await _validateAsync().ConfigureAwait(false);
                await _runAsync();
                if (_onSuccessAsync != null) await _onSuccessAsync().ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                var customException = exception as FinanceControlException;
                if (customException != null)
                {
                    _onCustomErrorWithLogger?.Invoke(customException, Logger);
                    if (_onCustomErrorWithLoggerAsync != null)
                        await _onCustomErrorWithLoggerAsync(customException, Logger).ConfigureAwait(false);
                    _onCustomError?.Invoke(customException);
                    if (_onCustomErrorAsync != null) 
                        await _onCustomErrorAsync(customException).ConfigureAwait(false);
                }

                var executeOnError = _executeOnError || customException == null;
                if (executeOnError)
                {
                    _onErrorWithLogger?.Invoke(customException, Logger);
                    if (_onErrorWithLoggerAsync != null) await _onErrorWithLoggerAsync(exception, Logger).ConfigureAwait(false);
                    _onError?.Invoke(exception);
                    if (_onErrorAsync != null) await _onErrorAsync(exception).ConfigureAwait(false);
                }

                if (_propagateException) throw;
            }
            finally
            {
                if (_alwaysAsync != null) await _alwaysAsync();
            }
        }
    }
}