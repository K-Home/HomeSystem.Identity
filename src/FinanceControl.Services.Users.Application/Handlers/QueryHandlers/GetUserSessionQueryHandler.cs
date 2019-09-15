using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceControl.Services.Users.Application.Dtos;
using FinanceControl.Services.Users.Application.Messages.Queries;
using FinanceControl.Services.Users.Application.Services.Base;
using FinanceControl.Services.Users.Domain.Aggregates;
using MediatR;

namespace FinanceControl.Services.Users.Application.Handlers.QueryHandlers
{
    public class GetUserSessionQueryHandler : IRequestHandler<GetUserSessionQuery, UserSessionDto>
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;

        public GetUserSessionQueryHandler(IMapper mapper, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _authenticationService = authenticationService;
        }
        
        public async Task<UserSessionDto> Handle(GetUserSessionQuery query, CancellationToken cancellationToken)
        {
            var session = await _authenticationService.GetSessionAsync(query.Id);
            var mappedSession = _mapper.Map<UserSession, UserSessionDto>(session);

            return mappedSession;
        }
    }
}