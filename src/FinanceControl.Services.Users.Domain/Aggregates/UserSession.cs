using System;
using FinanceControl.Services.Users.Domain.Exceptions;
using FinanceControl.Services.Users.Domain.Types;
using FinanceControl.Services.Users.Domain.Types.Base;

namespace FinanceControl.Services.Users.Domain.Aggregates
{
    public class UserSession : EntityBase, IEditable, ITimestampable
    {
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public string Key { get; private set; }
        public string UserAgent { get; private set; }
        public string IpAddress { get; private set; }
        public Guid? ParentId { get; private set; }
        public bool Refreshed { get; private set; }
        public bool Destroyed { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected UserSession()
        {
        }

        public UserSession(Guid id, Guid userId, string key = null,
            string ipAddress = null, string userAgent = null,
            Guid? parentId = null)
        {
            Id = id;
            UserId = userId;
            Key = key;
            UserAgent = userAgent;
            IpAddress = ipAddress;
            ParentId = parentId;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Destroy()
        {
            CheckIfAlreadyRefreshedOrDestroyed();
            Destroyed = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public UserSession Refresh(Guid newSessionId, string key, Guid parentId,
            string ipAddress = null, string userAgent = null)
        {
            CheckIfAlreadyRefreshedOrDestroyed();
            ParentId = parentId;
            Refreshed = true;
            UpdatedAt = DateTime.UtcNow;

            return new UserSession(newSessionId, UserId, key, ipAddress, userAgent, parentId);
        }

        private void CheckIfAlreadyRefreshedOrDestroyed()
        {
            if (Refreshed)
                throw new DomainException(Codes.SessionAlreadyRefreshed,
                    $"Session for user id: '{UserId}' " +
                    $"with key: '{Key}' has been already refreshed.");

            if (Destroyed)
                throw new DomainException(Codes.SessionAlreadyDestroyed,
                    $"Session for user id: '{UserId}' " +
                    $"with key: '{Key}' has been already destroyed.");
        }
    }
}