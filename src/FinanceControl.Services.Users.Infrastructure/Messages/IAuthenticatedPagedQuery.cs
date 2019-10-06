namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public interface IAuthenticatedPagedQuery : IAuthenticatedQuery, IPagedQuery
    {
    }

    public interface IAuthenticatedPagedQuery<out T> : IAuthenticatedQuery<T>, IPagedQuery<T>
    {
    }
}