using System.Threading.Channels;
using AnimeFranchises.Application.BackgroundTasks.Jobs;
using AnimeFranchises.Application.BackgroundTasks.Requests;
using AnimeFranchises.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AnimeFranchises.Infrastructure.BackgroundTasks;

public class CacheFranchiseIdsJob(Channel<CacheFranchiseIdsRequest> channel, IServiceProvider serviceProvider)
    : BackgroundService, ICacheFranchiseIdsJob
{
    private readonly Channel<CacheFranchiseIdsRequest> _channel = channel;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var request = await _channel.Reader.ReadAsync(stoppingToken);
            
            using var scope = _serviceProvider.CreateScope();
            var cache = scope.ServiceProvider.GetRequiredService<ICacheService>();
            
            var cachedIds = await cache.GetDataAsync<HashSet<Guid>>("anime-franchise-ids");
            
            if (cachedIds is null)  
                await cache.SetDataAsync("anime-franchise-ids", new HashSet<Guid>() { request.AnimeFranchiseId });
            else
            {
                cachedIds.Add(request.AnimeFranchiseId);
                await cache.SetDataAsync("anime-franchise-ids", cachedIds);
            }     
        }
    }

    public async Task PublishAsync(CacheFranchiseIdsRequest request, CancellationToken cancellationToken)
    {
        await _channel.Writer.WriteAsync(request, cancellationToken); 
    }
}