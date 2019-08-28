using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XSecure.Services.Users.Application.Dtos;
using XSecure.Services.Users.Application.Messages.Queries;
using XSecure.Services.Users.Application.Services.Base;
using XSecure.Services.Users.Domain.Aggregates;
using XSecure.Services.Users.Infrastructure.Pagination;

namespace XSecure.Services.Users.Application.Handlers.QueryHandlers
{
    public class BrowseUsersQueryHandler : IRequestHandler<BrowseUsersQuery, PagedResult<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public BrowseUsersQueryHandler(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<PagedResult<UserDto>> Handle(BrowseUsersQuery query, CancellationToken cancellationToken)
        {
            var users = await _userService.BrowseAsync();
            var mappedUsers = _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users).AsQueryable();
            var pagedUsers = await Pagination.PaginateAsync(mappedUsers, query.Page,
                query.PageSize, query.OrderBy, query.Ascending);

            return pagedUsers;
        }
    }
}