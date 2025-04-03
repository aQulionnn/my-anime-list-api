using AnimeFranchises.Application.Dtos.AnimeFranchiseInfoDtos;
using AnimeFranchises.Domain.Entities;
using AnimeFranchises.Domain.Interfaces;
using AnimeFranchises.Domain.Shared;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AnimeFranchises.Application.Features.AnimeFranchiseInfoFeatures.Commands.v2;

public class CreateAnimeFranchiseInfoCommandHandlerV2
    (IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateAnimeFranchiseInfoDto> validator) 
    : IRequestHandler<CreateAnimeFranchiseInfoCommandV2, Result<AnimeFranchiseInfo>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<CreateAnimeFranchiseInfoDto> _validator = validator;
    
    public async Task<Result<AnimeFranchiseInfo>> Handle(CreateAnimeFranchiseInfoCommandV2 request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request.CreateAnimeFranchiseInfoDto, cancellationToken);
        if (!validation.IsValid)
        {
            return Result<AnimeFranchiseInfo>.Failure(400, new Error("Validation failed", validation.Errors));
        }
        
        var franchiseExists = await _unitOfWork.AnimeFranchiseRepository.GetByIdAsync(request.CreateAnimeFranchiseInfoDto.AnimeFranchiseId);
        if (franchiseExists is null)
        {
            return Result<AnimeFranchiseInfo>.Failure(404, new Error("Anime franchise not found", null));
        }

        await _unitOfWork.BeginAsync();
        try
        {
            var animeFranchiseInfo = _mapper.Map<AnimeFranchiseInfo>(request.CreateAnimeFranchiseInfoDto);
            var result = await _unitOfWork.AnimeFranchiseInfoRepository.CreateAsync(animeFranchiseInfo);
            await _unitOfWork.CommitAsync();
            
            return Result<AnimeFranchiseInfo>.Success(result);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            return Result<AnimeFranchiseInfo>.Failure(500, new Error(ex.Message, null));
        }
    }
}

public record CreateAnimeFranchiseInfoCommandV2(CreateAnimeFranchiseInfoDto CreateAnimeFranchiseInfoDto) 
    : IRequest<Result<AnimeFranchiseInfo>>;
