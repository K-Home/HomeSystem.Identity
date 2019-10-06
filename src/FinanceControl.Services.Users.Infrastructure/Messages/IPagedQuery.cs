namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public interface IPagedQuery : IQuery
    {
        int Page { get; }
        int PageSize { get; }
        string OrderBy { get; }
        bool Ascending { get; }
    }

    public interface IPagedQuery<out T> : IQuery<T>
    {
        int Page { get; }
        int PageSize { get; }
        string OrderBy { get; }
        bool Ascending { get; }
    }
}