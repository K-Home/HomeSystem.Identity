using System;
using System.Threading.Tasks;

namespace FinanceControl.Services.Users.Infrastructure.Handlers
{
    public interface IHandlerTaskRunner
    {
        IHandlerTask Run(Action runAction);
        IHandlerTask Run(Func<Task> runAsyncAction);
    }
}