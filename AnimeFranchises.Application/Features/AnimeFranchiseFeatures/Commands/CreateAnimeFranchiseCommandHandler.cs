using AnimeFranchises.Application.Dtos.AnimeFranchiseDtos;
using AnimeFranchises.Domain.Entities;
using AnimeFranchises.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AnimeFranchises.Application.Features.AnimeFranchiseFeatures.Commands;

public class CreateAnimeFranchiseCommandHandler
    (IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateAnimeFranchiseDto> validator) 
    : IRequestHandler<CreateAnimeFranchiseCommand, AnimeFranchise>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<CreateAnimeFranchiseDto> _validator = validator;
    
    public async Task<AnimeFranchise> Handle(CreateAnimeFranchiseCommand request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request.CreateAnimeFranchiseDto, cancellationToken);
        if (!validation.IsValid)
        {
            throw new FluentValidation.ValidationException(validation.Errors);
        }

        await _unitOfWork.BeginAsync();
        try
        {
            var animeFranchise = _mapper.Map<AnimeFranchise>(request.CreateAnimeFranchiseDto);
            var result = await _unitOfWork.AnimeFranchiseRepository.CreateAsync(animeFranchise);
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

public record CreateAnimeFranchiseCommand(CreateAnimeFranchiseDto CreateAnimeFranchiseDto) 
    : IRequest<AnimeFranchise> { }