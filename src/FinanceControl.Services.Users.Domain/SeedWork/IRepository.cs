using FinanceControl.Services.Users.Domain.Types.Base;

namespace FinanceControl.Services.Users.Domain.SeedWork
{
    public interface IRepository<T> where T : IIdentifiable
    {
        IUnitOfWork UnitOfWork { get; }
    }
}