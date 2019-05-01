using HomeSystem.Services.Identity.Domain.Types;

namespace HomeSystem.Services.Identity.Domain.Enumerations
{
    public class OneTimeSecuredOperationType : Enumeration
    {
        public static OneTimeSecuredOperationType ResetPassword = new ResetPasswordOperation();
        public static OneTimeSecuredOperationType ActivateAccount = new ActivateAccountOperation();
        public static OneTimeSecuredOperationType LoginWith2Factor = new LoginWith2FactorOperation();

        public OneTimeSecuredOperationType(int id, string name)
            : base(id, name)
        {
        }

        private class ResetPasswordOperation : OneTimeSecuredOperationType
        {
            public ResetPasswordOperation() : base(1, "reset_password") { }
        }

        private class ActivateAccountOperation : OneTimeSecuredOperationType
        {
            public ActivateAccountOperation() : base(2, "activate_account") { }
        }

        private class LoginWith2FactorOperation : OneTimeSecuredOperationType
        {
            public LoginWith2FactorOperation() : base(3, "login_with_two_factor") { }
        }
    }
}