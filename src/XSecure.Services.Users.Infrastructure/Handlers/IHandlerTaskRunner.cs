using System;
using System.Threading.Tasks;

namespace XSecure.Services.Users.Infrastructure.Handlers
{
    public interface IHandlerTaskRunner
    {
        IHandlerTask Run(Action run);
        IHandlerTask Run(Func<Task> runAsync);         
    }
}