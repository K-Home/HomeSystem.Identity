using System;

namespace HomeSystem.Services.Identity.Infrastructure.Messages
{
    public interface IAuthenticatedQuery : IQuery
    {
        Guid UserId { get; }
    }

    public interface IAuthenticatedQuery<out T> : IQuery<T>
    {
        Guid UserId { get; }
    }
}
