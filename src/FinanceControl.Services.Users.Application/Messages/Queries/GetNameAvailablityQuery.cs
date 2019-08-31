using FinanceControl.Services.Users.Infrastructure.Messages;

namespace FinanceControl.Services.Users.Application.Messages.Queries
{
    public class GetNameAvailablityQuery : IQuery<bool>
    {
        public string Name { get; set; }
    }
}
