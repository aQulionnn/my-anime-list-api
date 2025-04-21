using AnimeFranchises.Application.BackgroundTasks.Requests;

namespace AnimeFranchises.Application.BackgroundTasks.Jobs;

public interface ICacheFranchiseIdsJob
{
    Task PublishAsync(CacheFranchiseIdsRequest request, CancellationToken cancellationToken);
}