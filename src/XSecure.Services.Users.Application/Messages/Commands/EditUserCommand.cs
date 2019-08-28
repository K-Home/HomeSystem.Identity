using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using XSecure.Services.Users.Domain.ValueObjects;
using XSecure.Services.Users.Infrastructure.Messages;

namespace XSecure.Services.Users.Application.Messages.Commands
{
    public class EditUserCommand : IAuthenticatedCommand
    {
        [DataMember]
        public Request Request { get; }
        
        [DataMember]
        public Guid UserId { get; }
        
        [DataMember]
        public string Email { get; }
        
        [DataMember]
        public string Name { get; }
        
        [DataMember]
        public string FirstName { get; }
        
        [DataMember]
        public string LastName { get; }
        
        [DataMember]
        public string PhoneNumber { get; }
        
        [DataMember]
        public UserAddress Address { get; }

        [JsonConstructor]
        public EditUserCommand(Guid userId, string email, string name, 
            string firstName, string lastName, string phoneNumber, UserAddress address)
        {
            Request = Request.New<SignUpCommand>();
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
