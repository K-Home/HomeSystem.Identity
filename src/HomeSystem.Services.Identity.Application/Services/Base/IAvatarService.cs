using HomeSystem.Services.Identity.Infrastructure.Files;
using System;
using System.Threading.Tasks;

namespace HomeSystem.Services.Identity.Application.Services.Base
{
    public interface IAvatarService
    {
         Task<string> GetUrlAsync(Guid userId);
         Task AddOrUpdateAsync(Guid userId, File avatar);
         Task RemoveAsync(Guid userId);
    }
}