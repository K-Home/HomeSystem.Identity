using FinanceControl.Services.Users.Application.Dtos;
using FinanceControl.Services.Users.Infrastructure.Messages;

namespace FinanceControl.Services.Users.Application.Messages.Queries
{
    public class GetUserByNameQuery : IQuery<UserDto>
    {
        public string Name { get; set; }
    }
}