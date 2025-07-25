using Azure.Storage.Blobs;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MessageBroker;

public static class MessagingExtension
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        var client = new BlobServiceClient(configuration.GetConnectionString("BlogStorage"));
        var repository = client.CreateMessageDataRepository("message-data", true);
        
        services.AddSingleton<IMessageDataRepository>(repository);
        
        services.AddMassTransit(mt =>
        {
            mt.SetKebabCaseEndpointNameFormatter();

            mt.UsingRabbitMq((context, config) =>
            {
                config.UseMessageData(repository);
                
                config.Host(configuration["RabbitMQ:Host"], "/", host =>
                {
                    host.Username(configuration["RabbitMQ:Username"]!);
                    host.Password(configuration["RabbitMQ:Password"]!);
                });

                config.ConfigureEndpoints(context);
            });
            
            mt.AddEntityFrameworkOutbox<MessagingDbContext>(o =>
            {
                o.QueryDelay = TimeSpan.FromSeconds(5);
                o.UsePostgres().UseBusOutbox();
                o.DuplicateDetectionWindow = TimeSpan.FromMinutes(1);
            });
        });

        services.AddDbContext<MessagingDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Database"), 
            sqlOptions => sqlOptions.MigrationsHistoryTable("__EFMigrationsHistory_Messaging")));
        
        return services;
    }
}