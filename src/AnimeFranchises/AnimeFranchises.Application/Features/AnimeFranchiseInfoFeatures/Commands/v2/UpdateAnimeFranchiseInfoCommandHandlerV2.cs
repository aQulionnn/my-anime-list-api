using AnimeFranchises.Application.Dtos.AnimeFranchiseInfoDtos;
using AnimeFranchises.Domain.Entities;
using AnimeFranchises.Domain.Interfaces;
using AnimeFranchises.Domain.Shared;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AnimeFranchises.Application.Features.AnimeFranchiseInfoFeatures.Commands.v2;

public class UpdateAnimeFranchiseInfoCommandHandlerV2
    (IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateAnimeFranchiseInfoDto> validator) 
    : IRequestHandler<UpdateAnimeFranchiseInfoCommandV2, Result<AnimeFranchiseInfo>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<UpdateAnimeFranchiseInfoDto> _validator = validator;
    
    public async Task<Result<AnimeFranchiseInfo>> Handle(UpdateAnimeFranchiseInfoCommandV2 request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request.UpdateAnimeFranchiseInfoDto, cancellationToken);
        if (!validation.IsValid)
        {
            return Result<AnimeFranchiseInfo>.Failure(Error.ValidationFailed(validation.Errors));
        }

        await _unitOfWork.BeginAsync();
        try
        {
            var animeFranchiseInfo = _mapper.Map<AnimeFranchiseInfo>(request.UpdateAnimeFranchiseInfoDto);
            var result = await _unitOfWork.AnimeFranchiseInfoRepository.UpdateAsync(request.Id, animeFranchiseInfo);
            await _unitOfWork.CommitAsync();
            
            return Result<AnimeFranchiseInfo>.Success(result);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            return Result<AnimeFranchiseInfo>.Failure(Error.InternalServerError(ex));
        }
    }
}

public record UpdateAnimeFranchiseInfoCommandV2(Guid Id, UpdateAnimeFranchiseInfoDto UpdateAnimeFranchiseInfoDto)
    : IRequest<Result<AnimeFranchiseInfo>>;
