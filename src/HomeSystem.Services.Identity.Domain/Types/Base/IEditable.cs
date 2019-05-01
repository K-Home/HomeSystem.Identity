using System;

namespace HomeSystem.Services.Identity.Domain.Types.Base
{
    public interface IEditable
    {
        DateTime UpdatedAt { get; }
    }
}