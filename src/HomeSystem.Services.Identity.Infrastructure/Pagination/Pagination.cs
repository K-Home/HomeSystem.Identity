using AutoMapper.QueryableExtensions;
using HomeSystem.Services.Identity.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HomeSystem.Services.Identity.Infrastructure.Pagination
{
    public static class Pagination
    {
        public static async Task<PagedResult<T>> PaginateAsync<T>(
            IQueryable<T> queryable,
            int page,
            int pageSize,
            string orderBy,
            bool ascending)
        {
            var skipAmount = pageSize * (page - 1);

            var projection = queryable
                .OrderByPropertyOrField(orderBy, ascending)
                .Skip(skipAmount)
                .Take(pageSize)
                .ProjectTo<T>();

            var totalNumberOfRecords = await queryable.CountAsync();
            var results = await projection.ToListAsync();
            var mod = totalNumberOfRecords % pageSize;
            var totalPageCount = totalNumberOfRecords / pageSize + (mod == 0 ? 0 : 1);

            return new PagedResult<T>
            {
                Results = results,
                PageNumber = page,
                PageSize = results.Count,
                TotalNumberOfPages = totalPageCount,
                TotalNumberOfRecords = totalNumberOfRecords,
            };
        }
    }
}
