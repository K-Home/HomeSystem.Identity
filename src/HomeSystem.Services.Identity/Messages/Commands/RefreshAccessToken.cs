using KShared.CQRS.Messages;

namespace HomeSystem.Services.Identity.Messages.Commands
{
    public class RefreshAccessToken : ICommand
    {
        public string Token { get; }

        public RefreshAccessToken(string token)
        {
            Token = token;
        }
    }
}