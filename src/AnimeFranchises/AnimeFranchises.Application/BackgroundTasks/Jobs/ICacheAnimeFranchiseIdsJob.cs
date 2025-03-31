using AnimeFranchises.Application.BackgroundTasks.Requests;

namespace AnimeFranchises.Application.BackgroundTasks.Jobs;

public interface ICacheAnimeFranchiseIdsJob
{
    Task PublishAsync(CacheAnimeFranchiseIdsRequest request, CancellationToken cancellationToken);
}