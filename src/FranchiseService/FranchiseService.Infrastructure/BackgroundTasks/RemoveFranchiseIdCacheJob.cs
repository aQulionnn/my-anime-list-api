using System.Threading.Channels;
using FranchiseService.Application.BackgroundTasks.Jobs;
using FranchiseService.Application.BackgroundTasks.Requests;
using FranchiseService.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FranchiseService.Infrastructure.BackgroundTasks;

public class RemoveFranchiseIdCacheJob(Channel<RemoveFranchiseIdRequest> channel, IServiceProvider serviceProvider)
    : BackgroundService, IRemoveFranchiseIdCacheJob
{
    private readonly Channel<RemoveFranchiseIdRequest> _channel = channel;
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var request = await _channel.Reader.ReadAsync(stoppingToken);

            var scope = _serviceProvider.CreateScope();
            var cache = scope.ServiceProvider.GetRequiredService<ICacheService>();
            
            var cachedIds = await cache.GetDataAsync<List<Guid>>("anime-franchise-ids");

            if (cachedIds is not null && cachedIds.Remove(request.AnimeFranchiseId))
                await cache.SetDataAsync("anime-franchise-ids", cachedIds, TimeSpan.FromHours(1));
        }   
    }

    public async Task PublishAsync(RemoveFranchiseIdRequest request, CancellationToken cancellationToken)
    {
        await _channel.Writer.WriteAsync(request, cancellationToken);
    }
}