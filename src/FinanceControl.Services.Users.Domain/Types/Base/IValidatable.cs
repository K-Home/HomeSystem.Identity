namespace FinanceControl.Services.Users.Domain.Types.Base
{
    public interface IValidatable
    {
        bool IsValid { get; }
    }
}