using AnimeFranchises.Application.Services;
using AnimeFranchises.Domain.Interfaces;
using Microsoft.Extensions.Hosting;

namespace AnimeFranchises.Infrastructure.BackgroundTasks;

public class RefreshAnimeFranchiseIdsCacheJob
    (ICacheService cache, IUnitOfWork unitOfWork) : BackgroundService
{
    private readonly ICacheService _cache = cache;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var cachedAnimeFranchiseIds = await _cache.GetDataAsync<HashSet<Guid>>("anime-franchise-ids");
            var animeFranchises = (await _unitOfWork.AnimeFranchiseRepository.GetAllAsync()).ToList();

            if (cachedAnimeFranchiseIds?.Count() != animeFranchises.Count)
            {
                var animeFranchiseIds = animeFranchises.Select(x => x.Id).ToList();
                await _cache.SetDataAsync("anime-franchise-ids", new HashSet<Guid>(animeFranchiseIds));
            }
            
            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}