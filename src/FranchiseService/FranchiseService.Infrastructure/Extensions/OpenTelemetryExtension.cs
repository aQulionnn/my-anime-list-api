using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace FranchiseService.Infrastructure.Extensions;

public static class OpenTelemetryExtension
{
    public static IServiceCollection AddOpenTelemetryConfiguration(this IServiceCollection services)
    {
        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService("AnimeFranchises"))
            .WithMetrics(metrics =>
            {
                metrics
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation();

                metrics.AddPrometheusExporter();
            })
            .WithTracing(tracing =>
            {
                tracing
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation();

                tracing
                    .AddOtlpExporter(options =>
                    {
                        options.Endpoint = new Uri("http://jaeger:4317");
                    });
            });
        
        return services;
    }
}