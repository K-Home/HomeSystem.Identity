using System;

namespace XSecure.Services.Users.Domain.Types.Base
{
    public interface ITimestampable
    {
        DateTime CreatedAt { get; }
    }
}