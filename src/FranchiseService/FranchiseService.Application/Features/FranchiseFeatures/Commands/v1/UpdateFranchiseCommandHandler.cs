using FranchiseService.Domain.Entities;
using FranchiseService.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using FranchiseService.Application.Dtos.FranchiseDtos;
using MediatR;

namespace FranchiseService.Application.Features.FranchiseFeatures.Commands.v1;

internal sealed class UpdateFranchiseCommandHandler
    (IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateFranchiseDto> validator) 
    : IRequestHandler<UpdateFranchiseCommand, Franchise>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<UpdateFranchiseDto> _validator = validator;
    
    public async Task<Franchise> Handle(UpdateFranchiseCommand request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request.UpdateFranchiseDto, cancellationToken);
        if (!validation.IsValid)
        {
            throw new FluentValidation.ValidationException(validation.Errors);
        }

        await _unitOfWork.BeginAsync();
        try
        {
            var animeFranchise = _mapper.Map<Franchise>(request.UpdateFranchiseDto);
            var result = await _unitOfWork.FranchiseRepository.UpdateAsync(request.Id, animeFranchise);
            await _unitOfWork.CommitAsync();
            
            return animeFranchise;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            throw new Exception(ex.Message);
        }
    }
}

public record UpdateFranchiseCommand(Guid Id, UpdateFranchiseDto UpdateFranchiseDto)
    : IRequest<Franchise> { }