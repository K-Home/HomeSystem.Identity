using XSecure.Services.Users.Domain.Types.Base;

namespace XSecure.Services.Users.Domain.SeedWork
{
    public interface IRepository<T> where T : IIdentifiable
    {
        IUnitOfWork UnitOfWork { get; }
    }
}