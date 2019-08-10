using HomeSystem.Services.Identity.Infrastructure.Messages;

namespace HomeSystem.Services.Identity.Application.Messages.Queries
{
    public class GetNameAvailablityQuery : IQuery<bool>
    {
        public string Name { get; set; }
    }
}
