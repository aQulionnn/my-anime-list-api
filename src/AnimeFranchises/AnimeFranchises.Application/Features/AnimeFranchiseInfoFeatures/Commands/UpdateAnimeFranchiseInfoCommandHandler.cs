using AnimeFranchises.Application.Dtos.AnimeFranchiseInfoDtos;
using AnimeFranchises.Domain.Entities;
using AnimeFranchises.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AnimeFranchises.Application.Features.AnimeFranchiseInfoFeatures.Commands;

public class UpdateAnimeFranchiseInfoCommandHandler
    (IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateAnimeFranchiseInfoDto> validator) 
    : IRequestHandler<UpdateAnimeFranchiseInfoCommand, AnimeFranchiseInfo>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<UpdateAnimeFranchiseInfoDto> _validator = validator;
    
    public async Task<AnimeFranchiseInfo> Handle(UpdateAnimeFranchiseInfoCommand request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request.UpdateAnimeFranchiseInfoDto, cancellationToken);
        if (!validation.IsValid)
        {
            throw new FluentValidation.ValidationException(validation.Errors);
        }

        await _unitOfWork.BeginAsync();
        try
        {
            var animeFranchiseInfo = _mapper.Map<AnimeFranchiseInfo>(request.UpdateAnimeFranchiseInfoDto);
            var result = await _unitOfWork.AnimeFranchiseInfoRepository.UpdateAsync(request.Id, animeFranchiseInfo);
            await _unitOfWork.CommitAsync();
            
            return animeFranchiseInfo;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            throw new Exception(ex.Message);
        }
    }
}

public record UpdateAnimeFranchiseInfoCommand(Guid Id, UpdateAnimeFranchiseInfoDto UpdateAnimeFranchiseInfoDto)
    : IRequest<AnimeFranchiseInfo>;