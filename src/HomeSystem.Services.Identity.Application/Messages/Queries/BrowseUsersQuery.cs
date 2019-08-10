using HomeSystem.Services.Identity.Application.Dtos;
using HomeSystem.Services.Identity.Infrastructure.Messages;
using HomeSystem.Services.Identity.Infrastructure.Pagination;

namespace HomeSystem.Services.Identity.Application.Messages.Queries
{
    public class BrowseUsersQuery : IQuery<PagedResult<UserDto>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public bool Ascending { get; set; }
    }
}
