using System;

namespace FinanceControl.Services.Users.Domain.Types.Base
{
    public interface IIdentifiable
    {
        Guid Id { get; }
    }
}