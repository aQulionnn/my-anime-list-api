using AnimeFranchises.Domain.Entities;
using AnimeFranchises.Domain.Interfaces;
using AnimeFranchises.Domain.Shared;
using MediatR;

namespace AnimeFranchises.Application.Features.AnimeFranchiseFeatures.Commands.v2;

public class DeleteAnimeFranchiseCommandHandlerV2
    (IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteAnimeFranchiseCommandV2, Result<AnimeFranchise>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<AnimeFranchise>> Handle(DeleteAnimeFranchiseCommandV2 request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginAsync();
        try
        {
            var result = await _unitOfWork.AnimeFranchiseRepository.DeleteAsync(request.Id);
            if (result == null)
            {
                return Result<AnimeFranchise>.Failure(Error.NotFound());
            }

            await _unitOfWork.CommitAsync();
            return Result<AnimeFranchise>.Success(result);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            return Result<AnimeFranchise>.Failure(Error.InternalServerError(ex));
        }
    }
}

public record DeleteAnimeFranchiseCommandV2(Guid Id)
    : IRequest<Result<AnimeFranchise>> { }
