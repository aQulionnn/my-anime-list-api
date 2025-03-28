using Microsoft.Extensions.Hosting;
using Serilog;

namespace AnimeSeries.Infrastructure.Extensions;

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