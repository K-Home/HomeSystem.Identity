using HomeSystem.Services.Identity.Domain.Types;

namespace HomeSystem.Services.Identity.Domain.Enumerations
{
    public class OneTimeSecuredOperations : Enumeration
    {
        public static OneTimeSecuredOperations ResetPassword = new ResetPasswordOperation();
        public static OneTimeSecuredOperations ActivateAccount = new ActivateAccountOperation();
        public static OneTimeSecuredOperations LoginWith2Factor = new LoginWith2FactorOperation();

        public OneTimeSecuredOperations(int id, string name)
            : base(id, name)
        {
        }

        private class ResetPasswordOperation : OneTimeSecuredOperations
        {
            public ResetPasswordOperation() : base(1, "reset_password") { }
        }

        private class ActivateAccountOperation : OneTimeSecuredOperations
        {
            public ActivateAccountOperation() : base(2, "activate_account") { }
        }

        private class LoginWith2FactorOperation : OneTimeSecuredOperations
        {
            public LoginWith2FactorOperation() : base(3, "login_with_two_factor") { }
        }
    }
}