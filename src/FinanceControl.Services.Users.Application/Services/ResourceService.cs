using System;
using System.Collections.Generic;
using FinanceControl.Services.Users.Application.Services.Base;
using FinanceControl.Services.Users.Infrastructure.Messages;

namespace FinanceControl.Services.Users.Application.Services
{
    public class ResourceService : IResourceService
    {
        private readonly string _service;
        private readonly IDictionary<Type, string> _resources;

        public ResourceService(string service, IDictionary<Type, string> resources)
        {
            _service = service;
            _resources = resources;
        }

        public Resource Resolve<T>(params object[] args) where T : class
            => Resource.Create(_service, string.Format(_resources[typeof(T)], args));
    }
}
