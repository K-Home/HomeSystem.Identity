using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using HomeSystem.Services.Identity.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HomeSystem.Services.Identity.Infrastructure.Pagination
{
    public static class Pagination
    {
        public static async Task<PagedResults<TReturn>> PaginateAsync<T, TReturn>(
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
                .ProjectTo<TReturn>();

            var totalNumberOfRecords = await queryable.CountAsync();
            var results = await projection.ToListAsync();
            var mod = totalNumberOfRecords % pageSize;
            var totalPageCount = totalNumberOfRecords / pageSize + (mod == 0 ? 0 : 1);

            return new PagedResults<TReturn>
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
