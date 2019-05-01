using System;
using HomeSystem.Services.Identity.Domain.Types.Base;

namespace HomeSystem.Services.Identity.Domain.Types
{
    public abstract class AggregateRootBase : IAggregateRoot
    {
        public Guid Id { get; set; }
    }
}