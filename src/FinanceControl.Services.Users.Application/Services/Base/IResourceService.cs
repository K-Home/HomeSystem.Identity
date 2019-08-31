using FinanceControl.Services.Users.Infrastructure.Messages;

namespace FinanceControl.Services.Users.Application.Services.Base
{
    public interface IResourceService
    {
        Resource Resolve<T>(params object[] args) where T : class;
    }
}
