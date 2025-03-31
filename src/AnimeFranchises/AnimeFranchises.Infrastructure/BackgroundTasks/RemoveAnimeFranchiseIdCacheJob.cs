using System.Threading.Channels;
using AnimeFranchises.Application.BackgroundTasks.Jobs;
using AnimeFranchises.Application.BackgroundTasks.Requests;
using AnimeFranchises.Application.Services;
using Microsoft.Extensions.Hosting;

namespace AnimeFranchises.Infrastructure.BackgroundTasks;

public class RemoveAnimeFranchiseIdCacheJob(Channel<RemoveAnimeFranchiseIdRequest> channel, ICacheService cache)
    : BackgroundService, IRemoveAnimeFranchiseIdCacheJob
{
    private readonly Channel<RemoveAnimeFranchiseIdRequest> _channel = channel;
    private readonly ICacheService _cache = cache;
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var request = await _channel.Reader.ReadAsync(stoppingToken);
            var cachedIds = await _cache.GetDataAsync<List<Guid>>("anime-franchise-ids");

            if (cachedIds is not null && cachedIds.Remove(request.AnimeFranchiseId))
                await _cache.SetDataAsync("anime-franchise-ids", cachedIds);
        }
    }

    public async Task PublishAsync(RemoveAnimeFranchiseIdRequest request, CancellationToken cancellationToken)
    {
        await _channel.Writer.WriteAsync(request, cancellationToken);
    }
}