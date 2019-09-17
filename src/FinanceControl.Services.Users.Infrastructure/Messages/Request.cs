using System;

namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public class Request
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public DateTime When { get; set; }


        public static Request From<T>(Request request)
        {
            return Create<T>(request.Id, request.Name);
        }

        public static Request New<T>()
        {
            return New<T>(Guid.NewGuid());
        }

        public static Request New<T>(Guid id)
        {
            return Create<T>(id, string.Empty);
        }

        public static Request Create<T>(Guid id, string name)
        {
            return new Request
            {
                Id = id,
                Name = typeof(T).Name,
                When = DateTime.UtcNow
            };
        }
    }
}