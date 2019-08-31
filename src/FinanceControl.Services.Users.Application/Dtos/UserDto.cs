using System;

namespace FinanceControl.Services.Users.Application.Dtos
{
    public class UserDto : BasicUserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string State { get; set; }
        public AddressDto Address { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}