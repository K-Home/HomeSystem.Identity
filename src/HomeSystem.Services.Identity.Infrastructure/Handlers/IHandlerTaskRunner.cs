using System;
using System.Threading.Tasks;

namespace HomeSystem.Services.Identity.Infrastructure.Handlers
{
    public interface IHandlerTaskRunner
    {
        IHandlerTask Run(Action run);
        IHandlerTask Run(Func<Task> runAsync);         
    }
}