using System;
using System.Threading.Tasks;
using HomeSystem.Services.Identity.Infrastructure.Files;

namespace HomeSystem.Services.Identity.Application.Services.Base
{
    public interface IAvatarService
    {
         Task<string> GetUrlAsync(Guid userId);
         Task AddOrUpdateAsync(Guid userId, File avatar);
         Task RemoveAsync(Guid userId);
    }
}