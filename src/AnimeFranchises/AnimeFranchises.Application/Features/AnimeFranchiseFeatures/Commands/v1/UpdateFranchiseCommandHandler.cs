using AnimeFranchises.Application.Dtos.FranchiseDtos;
using AnimeFranchises.Domain.Entities;
using AnimeFranchises.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AnimeFranchises.Application.Features.AnimeFranchiseFeatures.Commands.v1;

public class UpdateFranchiseCommandHandler
    (IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateFranchiseDto> validator) 
    : IRequestHandler<UpdateAnimeFranchiseCommand, Franchise>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<UpdateFranchiseDto> _validator = validator;
    
    public async Task<Franchise> Handle(UpdateAnimeFranchiseCommand request, CancellationToken cancellationToken)
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
            var result = await _unitOfWork.AnimeFranchiseRepository.UpdateAsync(request.Id, animeFranchise);
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

public record UpdateAnimeFranchiseCommand(Guid Id, UpdateFranchiseDto UpdateFranchiseDto)
    : IRequest<Franchise> { }