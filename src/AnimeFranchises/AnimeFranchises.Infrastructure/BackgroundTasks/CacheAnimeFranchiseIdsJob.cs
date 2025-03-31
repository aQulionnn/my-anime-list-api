using System.Threading.Channels;
using AnimeFranchises.Application.BackgroundTasks.Jobs;
using AnimeFranchises.Application.BackgroundTasks.Requests;
using AnimeFranchises.Application.Services;
using Microsoft.Extensions.Hosting;

namespace AnimeFranchises.Infrastructure.BackgroundTasks;

public class CacheAnimeFranchiseIdsJob(Channel<CacheAnimeFranchiseIdsRequest> channel, ICacheService cache)
    : BackgroundService, ICacheAnimeFranchiseIdsJob
{
    private readonly Channel<CacheAnimeFranchiseIdsRequest> _channel = channel;
    private readonly ICacheService _cache = cache;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var request = await _channel.Reader.ReadAsync(stoppingToken);
            var cachedIds = await _cache.GetDataAsync<List<Guid>>("anime-franchise-ids");
            
            if (cachedIds is null)  
                await _cache.SetDataAsync("anime-franchise-ids", new List<Guid>() { request.AnimeFranchiseId });
            else
            {
                cachedIds.Add(request.AnimeFranchiseId);
                await _cache.SetDataAsync("anime-franchise-ids", cachedIds);
            }     
        }
    }

    public async Task PublishAsync(CacheAnimeFranchiseIdsRequest request, CancellationToken cancellationToken)
    {
        await _channel.Writer.WriteAsync(request, cancellationToken); 
    }
}