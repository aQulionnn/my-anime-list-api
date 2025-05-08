using FranchiseService.Application.BackgroundTasks.Requests;

namespace FranchiseService.Application.BackgroundTasks.Jobs;

public interface ICacheFranchiseIdsJob
{
    Task PublishAsync(CacheFranchiseIdsRequest request, CancellationToken cancellationToken);
}