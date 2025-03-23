using AnimeFranchises.Application.Dtos.AnimeFranchiseInfoDtos;
using AnimeFranchises.Domain.Entities;
using AnimeFranchises.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AnimeFranchises.Application.Features.AnimeFranchiseInfoFeatures.Commands;

public class CreateAnimeFranchiseInfoCommandHandler
    (IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateAnimeFranchiseInfoDto> validator) 
    : IRequestHandler<CreateAnimeFranchiseInfoCommand, AnimeFranchiseInfo>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<CreateAnimeFranchiseInfoDto> _validator = validator;
    
    public async Task<AnimeFranchiseInfo> Handle(CreateAnimeFranchiseInfoCommand request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request.CreateAnimeFranchiseInfoDto, cancellationToken);
        if (!validation.IsValid)
        {
            throw new FluentValidation.ValidationException(validation.Errors);
        }

        await _unitOfWork.BeginAsync();
        try
        {
            var animeFranchiseInfo = _mapper.Map<AnimeFranchiseInfo>(request.CreateAnimeFranchiseInfoDto);
            var result = await _unitOfWork.AnimeFranchiseInfoRepository.CreateAsync(animeFranchiseInfo);
            await _unitOfWork.CommitAsync();
            
            return result;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            throw new Exception(ex.Message);
        }
    }
}

public record CreateAnimeFranchiseInfoCommand(CreateAnimeFranchiseInfoDto CreateAnimeFranchiseInfoDto) 
    : IRequest<AnimeFranchiseInfo>;