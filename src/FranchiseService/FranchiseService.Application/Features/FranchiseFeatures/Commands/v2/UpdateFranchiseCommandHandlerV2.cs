using FranchiseService.Domain.Entities;
using FranchiseService.Domain.Interfaces;
using FranchiseService.Domain.Shared;
using AutoMapper;
using FluentValidation;
using FranchiseService.Application.Dtos.FranchiseDtos;
using MediatR;

namespace FranchiseService.Application.Features.AnimeFranchiseFeatures.Commands.v2;

public class UpdateFranchiseCommandHandlerV2
    (IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateFranchiseDto> validator) 
    : IRequestHandler<UpdateAnimeFranchiseCommandV2, Result<Franchise>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<UpdateFranchiseDto> _validator = validator;

    public async Task<Result<Franchise>> Handle(UpdateAnimeFranchiseCommandV2 request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request.UpdateFranchiseDto, cancellationToken);
        if (!validation.IsValid)
        {
            return Result<Franchise>.Failure(Error.ValidationFailed(validation.Errors));
        }
        
        await _unitOfWork.BeginAsync();
        try
        {
            var animeFranchise = _mapper.Map<Franchise>(request.UpdateFranchiseDto);
            var result = await _unitOfWork.FranchiseRepository.UpdateAsync(request.Id, animeFranchise);

            if (result == null)
            {
                return Result<Franchise>.Failure(Error.NotFound());
            }

            await _unitOfWork.CommitAsync();
            return Result<Franchise>.Success(result);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            return Result<Franchise>.Failure(Error.InternalServerError(ex));
        }
    }
}

public record UpdateAnimeFranchiseCommandV2(Guid Id, UpdateFranchiseDto UpdateFranchiseDto)
    : IRequest<Result<Franchise>> { }
