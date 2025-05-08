using FranchiseService.Application.Dtos.FranchiseDtos;
using FranchiseService.Application.Dtos.FranchiseTranslationDtos;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Fallback;

namespace FranchiseService.Application.Extensions;

public static class FaultHandlingExtensions 
{
    public static IServiceCollection AddResiliencePipelines(this IServiceCollection services)
    {
        services.AddResiliencePipeline<string, FranchiseResponseDto>("anime-franchise-fallback",
            pipelineBuilder =>
            {
                pipelineBuilder.AddFallback(new FallbackStrategyOptions<FranchiseResponseDto>
                {
                    FallbackAction = _ =>
                        Outcome.FromResultAsValueTask<FranchiseResponseDto>(new FranchiseResponseDto())
                });
            });
        
        services.AddResiliencePipeline<string, FranchiseTranslationResponseDto>("anime-franchise-info-fallback",
            pipelineBuilder =>
            {
                pipelineBuilder.AddFallback(new FallbackStrategyOptions<FranchiseTranslationResponseDto>
                {
                    FallbackAction = _ =>
                        Outcome.FromResultAsValueTask<FranchiseTranslationResponseDto>(new FranchiseTranslationResponseDto())
                });
            });
        
        return services;
    }
}