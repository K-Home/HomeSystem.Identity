using System;

namespace FinanceControl.Services.Users.Domain.Types.Base
{
    public interface ITimestampable
    {
        DateTime CreatedAt { get; }
    }
}