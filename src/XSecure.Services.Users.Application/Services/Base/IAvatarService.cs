using System;
using System.Threading.Tasks;
using XSecure.Services.Users.Infrastructure.Files;

namespace XSecure.Services.Users.Application.Services.Base
{
    public interface IAvatarService
    {
         Task<string> GetUrlAsync(Guid userId);
         Task AddOrUpdateAsync(Guid userId, File avatar);
         Task RemoveAsync(Guid userId);
    }
}