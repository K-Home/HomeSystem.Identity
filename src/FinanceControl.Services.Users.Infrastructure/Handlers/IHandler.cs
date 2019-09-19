using System;
using System.Threading.Tasks;

namespace FinanceControl.Services.Users.Infrastructure.Handlers
{
    public interface IHandler
    {
        IHandlerTask Run(Action runAction);
        IHandlerTask Run(Func<Task> runAsyncAction);
        IHandlerTaskRunner Validate(Action validateAction);
        IHandlerTaskRunner Validate(Func<Task> validateAsyncAction);
        void ExecuteAll();
        Task ExecuteAllAsync();
    }
}