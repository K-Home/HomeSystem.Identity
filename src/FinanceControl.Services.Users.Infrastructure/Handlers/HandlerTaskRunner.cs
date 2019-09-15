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

        public HandlerTaskRunner(IHandler handler, Action validate, 
            Func<Task> validateAsync, ISet<IHandlerTask> handlerTasks)
        {
            _handler = handler;
            _validate = validate;
            _validateAsync = validateAsync;
            _handlerTasks = handlerTasks;
        }

        public IHandlerTask Run(Action run)
        {
            var handlerTask = new HandlerTask(_handler, run, _validate, _validateAsync);
            _handlerTasks.Add(handlerTask);

            return handlerTask;
        }

        public IHandlerTask Run(Func<Task> runAsync)
        {
            var handlerTask = new HandlerTask(_handler, runAsync, _validate, _validateAsync);
            _handlerTasks.Add(handlerTask);

            return handlerTask;
        }
    }
}