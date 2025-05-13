using FranchiseService.Application.Services;
using FranchiseService.Domain.Interfaces;
using FranchiseService.Infrastructure.Extensions;
using FranchiseService.Infrastructure.Data;
using FranchiseService.Infrastructure.Repositories;
using FranchiseService.Infrastructure.Services;
using MessageBroker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FranchiseService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FranchiseDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Database"), 
                sqlOptions => sqlOptions.MigrationsHistoryTable("__EFMigrationsHistory_Franchise"));
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IFranchiseRepository, FranchiseRepository>();
        services.AddScoped<IFranchiseTranslationRepository, FranchiseTranslationRepository>();

        services.AddScoped<ICacheService, RedisCacheService>();
        services.AddScoped<IMessagePublisher, MessagePublisher>();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = configuration["Redis:InstanceName"];
        });
        
        services.AddOpenTelemetryConfiguration();
        
        services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("Database")!)
            .AddRedis(configuration.GetConnectionString("Redis")!);

        services.AddMessaging(configuration);
        
        return services;
    }
}