using System;

namespace HomeSystem.Services.Identity.Domain.Types.Base
{
    public interface IIdentifiable 
    {
        Guid Id { get; }
    }
}