using AnimeFranchises.Application.BackgroundTasks.Requests;

namespace AnimeFranchises.Application.BackgroundTasks.Jobs;

public interface IRemoveAnimeFranchiseIdCacheJob
{
    Task PublishAsync(RemoveAnimeFranchiseIdRequest request, CancellationToken cancellationToken);
}