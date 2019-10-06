using System.Collections.Generic;
using System.Linq;

namespace FinanceControl.Services.Users.Infrastructure.Pagination
{
    public class PagedResult<T>
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalNumberOfPages { get; }
        public int TotalNumberOfRecords { get; }
        public IEnumerable<T> Results { get; }

        public bool IsEmpty => Results == null || !Results.Any();
        public bool IsNotEmpty => !IsEmpty;

        protected PagedResult()
        {
            Results = Enumerable.Empty<T>();
        }

        protected PagedResult(int pageNumber, int pageSize,
            int totalNumberOfPages, int totalNumberOfRecords,
            IEnumerable<T> results)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalNumberOfPages = totalNumberOfPages;
            TotalNumberOfRecords = totalNumberOfRecords;
            Results = results;
        }

        public static PagedResult<T> Create(int pageNumber, int pageSize,
            int totalNumberOfPages, int totalNumberOfRecords, IEnumerable<T> results)
        {
            var pagedResult =
                new PagedResult<T>(pageNumber, pageSize, totalNumberOfPages, totalNumberOfRecords, results);

            return pagedResult;
        }

        public static PagedResult<T> From(PagedResult<T> result, IEnumerable<T> items)
        {
            var pagedResult = new PagedResult<T>(result.PageNumber, result.PageSize,
                result.TotalNumberOfPages, result.TotalNumberOfRecords, items);

            return pagedResult;
        }

        public static PagedResult<T> Empty => new PagedResult<T>();
    }
}