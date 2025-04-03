using AnimeFranchises.Application.Dtos.AnimeFranchiseDtos;
using AnimeFranchises.Domain.Entities;
using AnimeFranchises.Domain.Interfaces;
using AnimeFranchises.Domain.Shared;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AnimeFranchises.Application.Features.AnimeFranchiseFeatures.Commands.v2;

public class UpdateAnimeFranchiseCommandHandlerV2
    (IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateAnimeFranchiseDto> validator) 
    : IRequestHandler<UpdateAnimeFranchiseCommandV2, Result<AnimeFranchise>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<UpdateAnimeFranchiseDto> _validator = validator;

    public async Task<Result<AnimeFranchise>> Handle(UpdateAnimeFranchiseCommandV2 request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request.UpdateAnimeFranchiseDto, cancellationToken);
        if (!validation.IsValid)
        {
            return Result<AnimeFranchise>.Failure(400, new Error("Validation failed", validation.Errors));
        }
        
        await _unitOfWork.BeginAsync();
        try
        {
            var animeFranchise = _mapper.Map<AnimeFranchise>(request.UpdateAnimeFranchiseDto);
            var result = await _unitOfWork.AnimeFranchiseRepository.UpdateAsync(request.Id, animeFranchise);

            if (result == null)
            {
                return Result<AnimeFranchise>.Failure(404, new Error("Anime franchise not found", null));
            }

            await _unitOfWork.CommitAsync();
            return Result<AnimeFranchise>.Success(result);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            return Result<AnimeFranchise>.Failure(500, new Error(ex.Message, ex));
        }
    }
}

public record UpdateAnimeFranchiseCommandV2(Guid Id, UpdateAnimeFranchiseDto UpdateAnimeFranchiseDto)
    : IRequest<Result<AnimeFranchise>> { }
