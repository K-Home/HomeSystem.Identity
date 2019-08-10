using HomeSystem.Services.Identity.Infrastructure.Logging.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Graylog;
using Serilog.Sinks.Graylog.Core.Transport;
using System;

namespace HomeSystem.Services.Identity.Infrastructure.Logging
{
    public static class LoggingRegistration
    {
        public static IWebHostBuilder UseLogging(this IWebHostBuilder webHostBuilder, string applicationName = null)
            => webHostBuilder.UseSerilog((context, loggerConfiguration) =>
            {
                var options = new LoggerOptions();
                context.Configuration.GetSection("logger").Bind(options);
                
                if (!Enum.TryParse<LogEventLevel>(options.Level, true, out var level))
                {
                    level = LogEventLevel.Information;
                }

                applicationName = string.IsNullOrWhiteSpace(applicationName)
                    ? Environment.GetEnvironmentVariable("APPLICATION_NAME")
                    : applicationName;

                loggerConfiguration.Enrich.FromLogContext()
                    .MinimumLevel.Is(level)
                    .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                    .Enrich.WithProperty("ApplicationName", applicationName);

                Configure(loggerConfiguration, options);
            });

        private static void Configure(LoggerConfiguration loggerConfiguration, LoggerOptions options)
        {
            var consoleOptions = options.Console ?? new ConsoleOptions();
            var fileOptions = options.File ?? new FileOptions();
            var grayLogOptions = options.GrayLog ?? new GrayLogOptions();

            if (consoleOptions.Enabled)
            {
                loggerConfiguration.WriteTo.Console();
            }

            if (fileOptions.Enabled)
            {
                var path = string.IsNullOrWhiteSpace(fileOptions.Path) ? "logs/logs.txt" : fileOptions.Path;

                if (!Enum.TryParse<RollingInterval>(fileOptions.Interval, true, out var interval))
                {
                    interval = RollingInterval.Day;
                }

                loggerConfiguration.WriteTo.File(path, rollingInterval: interval);
            }

            if (grayLogOptions.Enabled)
            {
                loggerConfiguration.WriteTo.Graylog(
                    new GraylogSinkOptions
                    {
                        HostnameOrAddress = grayLogOptions.Address,
                        Port = grayLogOptions.Port,
                        TransportType = TransportType.Http
                    });
            }
        }
    }
}