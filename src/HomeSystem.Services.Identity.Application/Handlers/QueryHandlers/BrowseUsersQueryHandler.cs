using AutoMapper;
using HomeSystem.Services.Identity.Application.Dtos;
using HomeSystem.Services.Identity.Application.Messages.Queries;
using HomeSystem.Services.Identity.Application.Services.Base;
using HomeSystem.Services.Identity.Domain.Aggregates;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HomeSystem.Services.Identity.Application.Handlers.QueryHandlers
{
    public class BrowseUsersQueryHandler : IRequestHandler<BrowseUsers, IEnumerable<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public BrowseUsersQueryHandler(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<IEnumerable<UserDto>> Handle(BrowseUsers query, CancellationToken cancellationToken)
        {
            var users = await _userService.BrowseAsync();
            var mappedUsers = _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);

            return mappedUsers;
        }
    }
}