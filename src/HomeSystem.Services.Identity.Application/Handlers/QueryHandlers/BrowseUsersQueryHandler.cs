using AutoMapper;
using HomeSystem.Services.Identity.Application.Dtos;
using HomeSystem.Services.Identity.Application.Messages.Queries;
using HomeSystem.Services.Identity.Application.Services.Base;
using HomeSystem.Services.Identity.Domain.Aggregates;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HomeSystem.Services.Identity.Infrastructure.Pagination;

namespace HomeSystem.Services.Identity.Application.Handlers.QueryHandlers
{
    public class BrowseUsersQueryHandler : IRequestHandler<BrowseUsers, PagedResult<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public BrowseUsersQueryHandler(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<PagedResult<UserDto>> Handle(BrowseUsers query, CancellationToken cancellationToken)
        {
            var users = await _userService.BrowseAsync();
            var mappedUsers = _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users).AsQueryable();
            var pagedUsers = await Pagination.PaginateAsync(mappedUsers, query.Page,
                query.PageSize, query.OrderBy, query.Ascending);

            return pagedUsers;
        }
    }
}