using System;
using System.Threading.Tasks;

namespace HomeSystem.Services.Identity.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task ExecuteAsync(Func<Task> query);
    }
}