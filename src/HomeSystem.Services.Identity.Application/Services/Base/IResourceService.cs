using HomeSystem.Services.Identity.Infrastructure.Messages;

namespace HomeSystem.Services.Identity.Application.Services.Base
{
    public interface IResourceService
    {
        Resource Resolve<T>(params object[] args) where T : class;
    }
}
