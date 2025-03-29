using AnimeFranchises.Application.Dtos.AnimeFranchiseDtos;
using AnimeFranchises.Application.Dtos.AnimeFranchiseInfoDtos;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Fallback;

namespace AnimeFranchises.Application.Extensions;

public static class FaultHandlingExtensions 
{
    public static IServiceCollection AddResiliencePipelines(this IServiceCollection services)
    {
        services.AddResiliencePipeline<string, AnimeFranchiseResponseDto>("anime-franchise-fallback",
            pipelineBuilder =>
            {
                pipelineBuilder.AddFallback(new FallbackStrategyOptions<AnimeFranchiseResponseDto>
                {
                    FallbackAction = _ =>
                        Outcome.FromResultAsValueTask<AnimeFranchiseResponseDto>(new AnimeFranchiseResponseDto())
                });
            });
        
        services.AddResiliencePipeline<string, AnimeFranchiseInfoResponseDto>("anime-franchise-info-fallback",
            pipelineBuilder =>
            {
                pipelineBuilder.AddFallback(new FallbackStrategyOptions<AnimeFranchiseInfoResponseDto>
                {
                    FallbackAction = _ =>
                        Outcome.FromResultAsValueTask<AnimeFranchiseInfoResponseDto>(new AnimeFranchiseInfoResponseDto())
                });
            });
        
        return services;
    }
}