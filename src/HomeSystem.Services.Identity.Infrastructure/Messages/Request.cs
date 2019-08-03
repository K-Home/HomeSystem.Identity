using System;

namespace HomeSystem.Services.Identity.Infrastructure.Messages
{
    public class Request
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public DateTime When { get; set; }


        public static Request From<T>(Request request)
            => Create<T>(request.Id, request.Name);

        public static Request New<T>() => New<T>(Guid.NewGuid());

        public static Request New<T>(Guid id) => Create<T>(id, string.Empty);

        public static Request Create<T>(Guid id, string name)
            => new Request
            {
                Id = id,
                Name = typeof(T).Name,
                When = DateTime.UtcNow
            };
    }
}