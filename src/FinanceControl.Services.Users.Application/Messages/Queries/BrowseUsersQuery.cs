using FinanceControl.Services.Users.Application.Dtos;
using FinanceControl.Services.Users.Infrastructure.Pagination;

namespace FinanceControl.Services.Users.Application.Messages.Queries
{
    public class BrowseUsersQuery : PagedQueryBase<PagedResult<UserDto>>
    {
    }
}