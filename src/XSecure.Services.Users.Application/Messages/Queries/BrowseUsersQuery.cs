using XSecure.Services.Users.Application.Dtos;
using XSecure.Services.Users.Infrastructure.Messages;
using XSecure.Services.Users.Infrastructure.Pagination;

namespace XSecure.Services.Users.Application.Messages.Queries
{
    public class BrowseUsersQuery : IQuery<PagedResult<UserDto>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public bool Ascending { get; set; }
    }
}
