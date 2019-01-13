using KShared.CQRS.Messages;

namespace HomeSystem.Services.Identity.Messages.Commands
{
    public class SignIn : ICommand
    {
        public string Email { get; }
        public string Password { get; }

        public SignIn(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}