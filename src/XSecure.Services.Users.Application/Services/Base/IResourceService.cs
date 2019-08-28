using XSecure.Services.Users.Infrastructure.Messages;

namespace XSecure.Services.Users.Application.Services.Base
{
    public interface IResourceService
    {
        Resource Resolve<T>(params object[] args) where T : class;
    }
}
