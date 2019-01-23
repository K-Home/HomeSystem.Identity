using System;
using KShared.CQRS.Messages;

namespace HomeSystem.Services.Identity.Messages.Commands
{
    public class SignUp : ICommand
    {
        public Guid Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; }
        public string Password { get; }
        public string Role { get; }

        public SignUp(Guid id, string email, string password, string role, 
            string firstName, string lastName)
        {
            Id = id;
            Email = email;
            Password = password;
            Role = role;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}