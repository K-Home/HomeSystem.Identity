using HomeSystem.Services.Identity.Infrastructure.Logging.Options;

namespace HomeSystem.Services.Identity.Infrastructure.Logging
{
    public class LoggerOptions
    {
        public string Level { get; set; }
        public ConsoleOptions Console { get; set; }
        public FileOptions File { get; set; }
    }
}