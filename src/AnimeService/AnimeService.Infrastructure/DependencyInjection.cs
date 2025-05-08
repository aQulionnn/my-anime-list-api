using AnimeService.Application.Services;
using AnimeService.Domain.Interfaces;
using AnimeService.Infrastructure.Data;
using AnimeService.Infrastructure.Repositories;
using AnimeService.Infrastructure.Services;
using MessageBroker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AnimeService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AnimeServiceDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Database"),
                sqlOptions => sqlOptions.MigrationsHistoryTable("__EFMigrationsHistory_AnimeSeries"));
        });
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAnimeRepository, AnimeRepository>();
        services.AddScoped<IAnimeTranslationRepository, AnimeTranslationRepository>();
        
        services.AddScoped<ICacheService, RedisCacheService>();
        
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = configuration["Redis:InstanceName"];
        });
        
        services.AddMessaging(configuration);
        
        return services;
    }
}