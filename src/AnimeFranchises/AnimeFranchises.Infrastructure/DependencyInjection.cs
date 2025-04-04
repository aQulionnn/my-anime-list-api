using AnimeFranchises.Application.Services;
using AnimeFranchises.Domain.Interfaces;
using AnimeFranchises.Infrastructure.Data;
using AnimeFranchises.Infrastructure.Extensions;
using AnimeFranchises.Infrastructure.Repositories;
using AnimeFranchises.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AnimeFranchises.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AnimeFranchiseDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Database"), 
                sqlOptions => sqlOptions.MigrationsHistoryTable("__EFMigrationsHistory_AnimeFranchise"));
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAnimeFranchiseRepository, AnimeFranchiseRepository>();
        services.AddScoped<IAnimeFranchiseInfoRepository, AnimeFranchiseInfoRepository>();

        services.AddScoped<ICacheService, RedisCacheService>();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = configuration["Redis:InstanceName"];
        });

        services.AddBackgroundTasks();
        services.AddOpenTelemetryConfiguration();
        
        return services;
    }
}