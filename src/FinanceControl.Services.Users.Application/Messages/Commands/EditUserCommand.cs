using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Domain.ValueObjects;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class EditUserCommand : IAuthenticatedCommand
    {
        public Request Request { get; }
        public Guid UserId { get; }
        public string Email { get; }
        public string Name { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string PhoneNumber { get; }
        public UserAddress Address { get; }

        [JsonConstructor]
        public EditUserCommand(Guid userId, string email, string name,
            string firstName, string lastName, string phoneNumber, UserAddress address)
        {
            Request = Request.New<EditUserCommand>();
            UserId = userId;
            Email = email;
            Name = name;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Address = address;
        }
    }
}