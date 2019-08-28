using System;

namespace XSecure.Services.Users.Domain.Types.Base
{
    public interface IIdentifiable 
    {
        Guid Id { get; }
    }
}