using System;
using HomeSystem.Services.Identity.Domain.Types.Base;

namespace HomeSystem.Services.Identity.Domain.Types
{
    public abstract class EntityBase : IEntity
    {
        public Guid Id { get; protected set; }
    }
}