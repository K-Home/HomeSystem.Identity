using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceControl.Services.Users.Application.Dtos;
using FinanceControl.Services.Users.Application.Messages.Queries;
using FinanceControl.Services.Users.Application.Services.Base;
using FinanceControl.Services.Users.Domain.Aggregates;
using FinanceControl.Services.Users.Domain.Extensions;
using FinanceControl.Services.Users.Infrastructure.Pagination;
using MediatR;

namespace FinanceControl.Services.Users.Application.Handlers.QueryHandlers
{
    public class BrowseUsersQueryHandler : IRequestHandler<BrowseUsersQuery, PagedResult<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public BrowseUsersQueryHandler(IMapper mapper, IUserService userService)
        {
            _mapper = mapper.CheckIfNotEmpty();
            _userService = userService.CheckIfNotEmpty();
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