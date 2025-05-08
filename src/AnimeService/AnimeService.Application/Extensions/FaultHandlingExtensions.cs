using AnimeService.Application.Dtos.AnimeDtos;
using AnimeService.Application.Dtos.AnimeSerialInfoDtos;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Fallback;

namespace AnimeService.Application.Extensions;

public static class FaultHandlingExtensions
{
    public static IServiceCollection AddResiliencePipelines(this IServiceCollection services)
    {
        services.AddResiliencePipeline<string, AnimeResponseDto>("anime-fallback",
            pipelineBuilder =>
            {
                pipelineBuilder.AddFallback(new FallbackStrategyOptions<AnimeResponseDto>
                {
                    FallbackAction = _ =>
                        Outcome.FromResultAsValueTask<AnimeResponseDto>(new AnimeResponseDto())
                });
            });
        
        services.AddResiliencePipeline<string, AnimeTranslationResponseDto>("anime-translation-fallback",
            pipelineBuilder =>
            {
                pipelineBuilder.AddFallback(new FallbackStrategyOptions<AnimeTranslationResponseDto>
                {
                    FallbackAction = _ =>
                        Outcome.FromResultAsValueTask<AnimeTranslationResponseDto>(new AnimeTranslationResponseDto())
                });
            });
        
        return services;
    }
}