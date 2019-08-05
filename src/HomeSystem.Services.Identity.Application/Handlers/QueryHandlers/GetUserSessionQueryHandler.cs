using AutoMapper;
using HomeSystem.Services.Identity.Application.Dtos;
using HomeSystem.Services.Identity.Application.Messages.Queries;
using HomeSystem.Services.Identity.Application.Services.Base;
using HomeSystem.Services.Identity.Domain.Aggregates;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HomeSystem.Services.Identity.Application.Handlers.QueryHandlers
{
    public class GetUserSessionQueryHandler : IRequestHandler<GetUserSession, UserSessionDto>
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;

        public GetUserSessionQueryHandler(IMapper mapper, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _authenticationService = authenticationService;
        }
        
        public async Task<UserSessionDto> Handle(GetUserSession query, CancellationToken cancellationToken)
        {
            var session = await _authenticationService.GetSessionAsync(query.Id);
            var mappedSession = _mapper.Map<UserSession, UserSessionDto>(session);

            return mappedSession;
        }
    }
}