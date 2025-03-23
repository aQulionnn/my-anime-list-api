using AnimeFranchises.Domain.Interfaces;
using AnimeFranchises.Infrastructure.Data;
using AnimeFranchises.Infrastructure.Repositories;
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
            options.UseNpgsql(configuration.GetConnectionString("Database"));
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAnimeFranchiseRepository, AnimeFranchiseRepository>();
        services.AddScoped<IAnimeFranchiseInfoRepository, AnimeFranchiseInfoRepository>();
        
        return services;
    }
}