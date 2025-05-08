using Microsoft.Extensions.Hosting;
using Serilog;

namespace AnimeService.Infrastructure.Extensions;

public static class LoggingExtension
{
    public static void AddSerilogConfiguration(this IHostBuilder host)
    {
        host.UseSerilog((context, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration);
        });
    }
}