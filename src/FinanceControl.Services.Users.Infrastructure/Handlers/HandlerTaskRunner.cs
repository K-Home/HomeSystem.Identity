using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceControl.Services.Users.Infrastructure.Handlers
{
    public class HandlerTaskRunner : IHandlerTaskRunner
    {
        private readonly IHandler _handler;
        private readonly Action _validate;
        private readonly Func<Task> _validateAsync;
        private readonly ISet<IHandlerTask> _handlerTasks;

        public HandlerTaskRunner(IHandler handler, Action validateAction,
            Func<Task> validateAsyncAction, ISet<IHandlerTask> handlerTasks)
        {
            _handler = handler;
            _validate = validateAction;
            _validateAsync = validateAsyncAction;
            _handlerTasks = handlerTasks;
        }

        public IHandlerTask Run(Action runAction)
        {
            var handlerTask = new HandlerTask(_handler, runAction, _validate, _validateAsync);
            _handlerTasks.Add(handlerTask);

            return handlerTask;
        }

        public IHandlerTask Run(Func<Task> runAsyncAction)
        {
            var handlerTask = new HandlerTask(_handler, runAsyncAction, _validate, _validateAsync);
            _handlerTasks.Add(handlerTask);

            return handlerTask;
        }
    }
}