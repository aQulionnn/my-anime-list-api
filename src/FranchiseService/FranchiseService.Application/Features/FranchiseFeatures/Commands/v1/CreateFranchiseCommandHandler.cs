using FranchiseService.Domain.Entities;
using FranchiseService.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using FranchiseService.Application.Dtos.FranchiseDtos;
using MediatR;

namespace FranchiseService.Application.Features.FranchiseFeatures.Commands.v1;

public class CreateFranchiseCommandHandler
    (
        IUnitOfWork unitOfWork, 
        IMapper mapper, 
        IValidator<CreateFranchiseDto> validator
    ) 
    : IRequestHandler<CreateFranchiseCommand, Franchise>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<CreateFranchiseDto> _validator = validator;
    
    public async Task<Franchise> Handle(CreateFranchiseCommand request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request.CreateFranchiseDto, cancellationToken);
        if (!validation.IsValid)
        {
            throw new FluentValidation.ValidationException(validation.Errors);
        }

        Franchise? result = null;
        
        await _unitOfWork.BeginAsync();
        try
        {
            var animeFranchise = _mapper.Map<Franchise>(request.CreateFranchiseDto);
            result = await _unitOfWork.FranchiseRepository.CreateAsync(animeFranchise);
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

public record CreateFranchiseCommand(CreateFranchiseDto CreateFranchiseDto) 
    : IRequest<Franchise> { }