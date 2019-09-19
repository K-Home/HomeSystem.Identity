using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceControl.Services.Users.Infrastructure.Handlers
{
    public class Handler : IHandler
    {
        private readonly ISet<IHandlerTask> _handlerTasks = new HashSet<IHandlerTask>();

        public IHandlerTask Run(Action runAction)
        {
            var handlerTask = new HandlerTask(this, runAction);
            _handlerTasks.Add(handlerTask);

            return handlerTask;
        }

        public IHandlerTask Run(Func<Task> runActionAsync)
        {
            var handlerTask = new HandlerTask(this, runActionAsync);
            _handlerTasks.Add(handlerTask);

            return handlerTask;
        }

        public IHandlerTaskRunner Validate(Action validateAction)
        {
            return new HandlerTaskRunner(this, validateAction, null, _handlerTasks);
        }

        public IHandlerTaskRunner Validate(Func<Task> validateActionAsync)
        {
            return new HandlerTaskRunner(this, null, validateActionAsync, _handlerTasks);
        }

        public void ExecuteAll()
        {
            foreach (var handlerTask in _handlerTasks) handlerTask.Execute();
            {
                _handlerTasks.Clear();
            }
        }

        public async Task ExecuteAllAsync()
        {
            foreach (var handlerTask in _handlerTasks) await handlerTask.ExecuteAsync();
            {
                _handlerTasks.Clear();
            }
        }
    }
}