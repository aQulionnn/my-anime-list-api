using AnimeFranchises.Application.Dtos.AnimeFranchiseDtos;
using AnimeFranchises.Domain.Entities;
using AnimeFranchises.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AnimeFranchises.Application.Features.AnimeFranchiseFeatures.Commands.v1;

public class UpdateAnimeFranchiseCommandHandler
    (IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateAnimeFranchiseDto> validator) 
    : IRequestHandler<UpdateAnimeFranchiseCommand, AnimeFranchise>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<UpdateAnimeFranchiseDto> _validator = validator;
    
    public async Task<AnimeFranchise> Handle(UpdateAnimeFranchiseCommand request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request.UpdateAnimeFranchiseDto, cancellationToken);
        if (!validation.IsValid)
        {
            throw new FluentValidation.ValidationException(validation.Errors);
        }

        await _unitOfWork.BeginAsync();
        try
        {
            var animeFranchise = _mapper.Map<AnimeFranchise>(request.UpdateAnimeFranchiseDto);
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

public record UpdateAnimeFranchiseCommand(Guid Id, UpdateAnimeFranchiseDto UpdateAnimeFranchiseDto)
    : IRequest<AnimeFranchise> { }