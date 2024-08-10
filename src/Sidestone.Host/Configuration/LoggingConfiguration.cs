using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Exceptions;

namespace Sidestone.Host.Configuration
{
    public static class LoggingConfiguration
    {
        public static void ConfigureSerilog(
            this WebApplicationBuilder builder,
            IWebHostEnvironment env,
            Logger? serilogLogger = null,
            string? outputTemplate = null
        )
        {
            builder.Logging.ClearProviders();

            var loggingLevelSwitch = GetLoggingLevelSwitch(env);

            var logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(loggingLevelSwitch)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithProperty("ApplicationName", env.ApplicationName)
                .WriteTo.Console(
                    outputTemplate: outputTemplate ?? "[{Timestamp:HH:mm:ss}] [{Level:u3}] {SourceContext}: {Message}{NewLine}{Exception}"
                )
                .CreateLogger();

            builder.Host.UseSerilog(serilogLogger ?? logger);
        }

        private static LoggingLevelSwitch GetLoggingLevelSwitch(IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                return new LoggingLevelSwitch(LogEventLevel.Warning);
            }
            if (env.IsProduction())
            {
                return new LoggingLevelSwitch(LogEventLevel.Information);
            }

            return new LoggingLevelSwitch(LogEventLevel.Information);
        }
    }
}
