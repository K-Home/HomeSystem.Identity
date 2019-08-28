using System;

namespace XSecure.Services.Users.Domain.Types.Base
{
    public interface IEditable
    {
        DateTime UpdatedAt { get; }
    }
}