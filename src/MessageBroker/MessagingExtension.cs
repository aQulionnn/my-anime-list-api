using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MessageBroker;

public static class MessagingExtension
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(mt =>
        {
            mt.SetKebabCaseEndpointNameFormatter();

            mt.UsingRabbitMq((context, config) =>
            {
                config.Host(configuration["RabbitMQ:Host"], "/", host =>
                {
                    host.Username(configuration["RabbitMQ:Username"]!);
                    host.Password(configuration["RabbitMQ:Password"]!);
                });

                config.ConfigureEndpoints(context);
            });
        });
        
        return services;
    }
}