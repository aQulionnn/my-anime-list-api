using AnimeFranchises.Application.BackgroundTasks.Requests;

namespace AnimeFranchises.Application.BackgroundTasks.Jobs;

public interface IRemoveFranchiseIdCacheJob
{
    Task PublishAsync(RemoveFranchiseIdRequest request, CancellationToken cancellationToken);
}