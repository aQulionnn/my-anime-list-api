using FranchiseService.Application.Services;
using FranchiseService.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FranchiseService.Infrastructure.BackgroundTasks;

public class RefreshAnimeFranchiseIdsCacheJob
    (IServiceProvider serviceProvider) : BackgroundService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var cache = scope.ServiceProvider.GetRequiredService<ICacheService>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            
            var cachedAnimeFranchiseIds = await cache.GetDataAsync<HashSet<Guid>>("anime-franchise-ids");
            var animeFranchises = (await unitOfWork.FranchiseRepository.GetAllAsync()).ToList();

            if (cachedAnimeFranchiseIds?.Count() != animeFranchises.Count)
            {
                var animeFranchiseIds = animeFranchises.Select(x => x.Id).ToList();
                await cache.SetDataAsync("anime-franchise-ids", new HashSet<Guid>(animeFranchiseIds));
            }
            
            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}