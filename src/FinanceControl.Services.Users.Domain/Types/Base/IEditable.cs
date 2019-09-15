using System;

namespace FinanceControl.Services.Users.Domain.Types.Base
{
    public interface IEditable
    {
        DateTime UpdatedAt { get; }
    }
}