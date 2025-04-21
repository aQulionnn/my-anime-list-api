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
        services.AddSingleton<Channel<CacheFranchiseIdsRequest>>(
            _ => Channel.CreateUnbounded<CacheFranchiseIdsRequest>(new UnboundedChannelOptions
            {
                SingleReader = false,
                AllowSynchronousContinuations = true,
            }));
        
        services.AddSingleton<Channel<RemoveFranchiseIdRequest>>(
            _ => Channel.CreateUnbounded<RemoveFranchiseIdRequest>(new UnboundedChannelOptions
            {
                SingleReader = false,
                AllowSynchronousContinuations = true,
            }));

        services.AddSingleton<ICacheFranchiseIdsJob, CacheFranchiseIdsJob>();
        services.AddSingleton<IRemoveFranchiseIdCacheJob, RemoveFranchiseIdCacheJob>();
        
        services.AddHostedService<CacheFranchiseIdsJob>();
        services.AddHostedService<RemoveFranchiseIdCacheJob>();
        services.AddHostedService<RefreshAnimeFranchiseIdsCacheJob>();
        
        return services;
    }
}