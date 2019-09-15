using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceControl.Services.Users.Infrastructure.Handlers
{
    public class Handler : IHandler
    {
        private readonly ISet<IHandlerTask> _handlerTasks = new HashSet<IHandlerTask>();

        public IHandlerTask Run(Action run)
        {
            var handlerTask = new HandlerTask(this, run);
            _handlerTasks.Add(handlerTask);

            return handlerTask;
        }

        public IHandlerTask Run(Func<Task> runAsync)
        {
            var handlerTask = new HandlerTask(this, runAsync);
            _handlerTasks.Add(handlerTask);

            return handlerTask;
        }

        public IHandlerTaskRunner Validate(Action validate)
            => new HandlerTaskRunner(this, validate, null, _handlerTasks);

        public IHandlerTaskRunner Validate(Func<Task> validateAsync)
            => new HandlerTaskRunner(this, null, validateAsync, _handlerTasks);

        public void ExecuteAll()
        {
            foreach (var handlerTask in _handlerTasks)
            {
                handlerTask.Execute();
            }
            _handlerTasks.Clear();
        }

        public async Task ExecuteAllAsync()
        {
            foreach (var handlerTask in _handlerTasks)
            {
                await handlerTask.ExecuteAsync();
            }
            _handlerTasks.Clear();
        }
    }
}