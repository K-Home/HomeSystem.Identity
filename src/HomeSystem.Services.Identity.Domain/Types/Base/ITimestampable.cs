using System;

namespace HomeSystem.Services.Identity.Domain.Types.Base
{
    public interface ITimestampable
    {
        DateTime CreatedAt { get; }
    }
}