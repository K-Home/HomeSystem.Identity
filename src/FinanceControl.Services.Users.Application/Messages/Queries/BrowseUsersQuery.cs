using FinanceControl.Services.Users.Application.Dtos;
using FinanceControl.Services.Users.Infrastructure.Messages;
using FinanceControl.Services.Users.Infrastructure.Pagination;

namespace FinanceControl.Services.Users.Application.Messages.Queries
{
    public class BrowseUsersQuery : IQuery<PagedResult<UserDto>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public bool Ascending { get; set; }
    }
}