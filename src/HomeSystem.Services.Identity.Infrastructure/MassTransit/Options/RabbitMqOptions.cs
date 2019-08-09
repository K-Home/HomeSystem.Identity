namespace HomeSystem.Services.Identity.Infrastructure.MassTransit.Options
{
    public class RabbitMqOptions 
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string HostAddress { get; set; }
        public string QueueName { get; set; }
        public ushort PrefetchCount { get; set; }
        public int RetryIntervalMinValue { get; set; }
        public int RetryIntervalMaxValue { get; set; }
    }
}
