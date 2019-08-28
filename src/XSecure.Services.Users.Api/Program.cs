using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using XSecure.Services.Users.Infrastructure.Logging;

namespace XSecure.Services.Users.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseLogging()
                .UseStartup<Startup>();
    }
}