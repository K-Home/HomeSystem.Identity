using System;
using System.Threading.Tasks;

namespace FinanceControl.Services.Users.Infrastructure.Handlers
{
    public interface IHandlerTaskRunner
    {
        IHandlerTask Run(Action run);
        IHandlerTask Run(Func<Task> runAsync);         
    }
}