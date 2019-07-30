using System;

namespace HomeSystem.Services.Identity.Application.Dtos
{
    public class SessionDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Key { get; set; }
        public string UserAgent { get; set; }
        public string IpAddress { get; set; }
        public Guid? ParentId { get; set; }
        public bool Refreshed { get; set; }
        public bool Destroyed { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}