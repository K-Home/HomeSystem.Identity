using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using FinanceControl.Services.Users.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FinanceControl.Services.Users.Infrastructure.Pagination
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

            return PagedResult<T>.Create(page, results.Count, 
                totalPageCount, totalNumberOfRecords, results);
        }
    }
}