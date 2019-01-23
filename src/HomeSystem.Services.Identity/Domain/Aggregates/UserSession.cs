using System;
using HomeSystem.Services.Identity.Exceptions;
using KShared.Domain.BaseClasses;
using KShared.Exceptions.Exceptions;

namespace HomeSystem.Services.Identity.Domain.Aggregates
{
    public class UserSession : Entity, IEditable, ITimestampable
    {
        public Guid UserId { get; private set; }
        public string Key { get; private set; }
        public string UserAgent { get; private set; }
        public string IpAddress { get; private set; }
        public Guid? ParentId { get; private set; }
        public bool Refreshed { get; private set; }
        public bool Destroyed { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime CreatedAt { get; }

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
            {
                throw new DomainException(Codes.SessionAlreadyRefreshed,
                    $"Session for user id: '{UserId}' " +
                    $"with key: '{Key}' has been already refreshed.");
            }
            if (Destroyed)
            {
                throw new DomainException(Codes.SessionAlreadyDestroyed,
                    $"Session for user id: '{UserId}' " +
                    $"with key: '{Key}' has been already destroyed.");
            }
        }
    }
}