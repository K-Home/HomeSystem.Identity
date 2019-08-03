using System;
using System.Runtime.Serialization;
using HomeSystem.Services.Identity.Domain.ValueObjects;
using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
{
    public class EditUser : IAuthenticatedCommand
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
        public EditUser(Request request, Guid userId, string email, string name, 
            string firstName, string lastName, string phoneNumber, UserAddress address)
        {
            Request = request;
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
