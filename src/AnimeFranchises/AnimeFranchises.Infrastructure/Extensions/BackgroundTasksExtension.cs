using System.Threading.Channels;
using AnimeFranchises.Application.BackgroundTasks.Jobs;
using AnimeFranchises.Application.BackgroundTasks.Requests;
using AnimeFranchises.Infrastructure.BackgroundTasks;
using Microsoft.Extensions.DependencyInjection;

namespace AnimeFranchises.Infrastructure.Extensions;

public static class BackgroundTasksExtension
{
    public static IServiceCollection AddBackgroundTasks(this IServiceCollection services)
    {
        services.AddSingleton<Channel<CacheAnimeFranchiseIdsRequest>>(
            _ => Channel.CreateUnbounded<CacheAnimeFranchiseIdsRequest>(new UnboundedChannelOptions
            {
                SingleReader = false,
                AllowSynchronousContinuations = true,
            }));
        
        services.AddSingleton<Channel<RemoveAnimeFranchiseIdRequest>>(
            _ => Channel.CreateUnbounded<RemoveAnimeFranchiseIdRequest>(new UnboundedChannelOptions
            {
                SingleReader = false,
                AllowSynchronousContinuations = true,
            }));

        services.AddSingleton<ICacheAnimeFranchiseIdsJob, CacheAnimeFranchiseIdsJob>();
        services.AddSingleton<IRemoveAnimeFranchiseIdCacheJob, RemoveAnimeFranchiseIdCacheJob>();
        
        services.AddHostedService<CacheAnimeFranchiseIdsJob>();
        services.AddHostedService<RemoveAnimeFranchiseIdCacheJob>();
        
        return services;
    }
}