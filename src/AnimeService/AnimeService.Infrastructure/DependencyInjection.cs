using AnimeService.Application.Services;
using AnimeService.Domain.Interfaces;
using AnimeService.Infrastructure.Data;
using AnimeService.Infrastructure.Repositories;
using AnimeService.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AnimeService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AnimeSeriesDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Database"),
                sqlOptions => sqlOptions.MigrationsHistoryTable("__EFMigrationsHistory_AnimeSeries"));
        });
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAnimeSerialRepository, AnimeSerialRepository>();
        services.AddScoped<IAnimeSerialInfoRepository, AnimeSerialInfoRepository>();
        services.AddScoped<IReWatchedAnimeSerialRepository, ReWatchedAnimeSerialRepository>();
        
        services.AddScoped<ICacheService, RedisCacheService>();
        
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = configuration["Redis:InstanceName"];
        });
        
        return services;
    }
}