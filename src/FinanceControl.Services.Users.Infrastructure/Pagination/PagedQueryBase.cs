using FinanceControl.Services.Users.Infrastructure.Messages;

namespace FinanceControl.Services.Users.Infrastructure.Pagination
{
    public abstract class PagedQueryBase : IPagedQuery
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public bool Ascending { get; set; }
    }

    public abstract class PagedQueryBase<T> : IPagedQuery<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public bool Ascending { get; set; }
    }
}