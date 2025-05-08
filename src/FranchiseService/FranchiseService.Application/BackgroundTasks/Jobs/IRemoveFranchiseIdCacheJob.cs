using FranchiseService.Application.BackgroundTasks.Requests;

namespace FranchiseService.Application.BackgroundTasks.Jobs;

public interface IRemoveFranchiseIdCacheJob
{
    Task PublishAsync(RemoveFranchiseIdRequest request, CancellationToken cancellationToken);
}