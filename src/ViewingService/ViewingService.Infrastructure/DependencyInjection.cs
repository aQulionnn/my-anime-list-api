using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ViewingService.Infrastructure.Data;

namespace ViewingService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ViewingServiceDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Database"),
                sqlOptions => sqlOptions.MigrationsHistoryTable("__EFMigrationsHistory_ViewingService"));
        });

        return services;
    }
}