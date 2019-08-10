using HomeSystem.Services.Identity.Domain.Types.Base;

namespace HomeSystem.Services.Identity.Domain.SeedWork
{
    public interface IRepository<T> where T : IIdentifiable
    {
        IUnitOfWork UnitOfWork { get; }
    }
}