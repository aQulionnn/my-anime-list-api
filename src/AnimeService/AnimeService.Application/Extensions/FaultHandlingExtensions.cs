using AnimeService.Application.Dtos.AnimeSerialDtos;
using AnimeService.Application.Dtos.AnimeSerialInfoDtos;
using AnimeService.Application.Dtos.ReWatchedAnimeSerialDtos;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Fallback;

namespace AnimeService.Application.Extensions;

public static class FaultHandlingExtensions
{
    public static IServiceCollection AddResiliencePipelines(this IServiceCollection services)
    {
        services.AddResiliencePipeline<string, AnimeSerialResponseDto>("anime-serial-fallback",
            pipelineBuilder =>
            {
                pipelineBuilder.AddFallback(new FallbackStrategyOptions<AnimeSerialResponseDto>
                {
                    FallbackAction = _ =>
                        Outcome.FromResultAsValueTask<AnimeSerialResponseDto>(new AnimeSerialResponseDto())
                });
            });
        
        services.AddResiliencePipeline<string, AnimeSerialInfoResponseDto>("anime-serial-info-fallback",
            pipelineBuilder =>
            {
                pipelineBuilder.AddFallback(new FallbackStrategyOptions<AnimeSerialInfoResponseDto>
                {
                    FallbackAction = _ =>
                        Outcome.FromResultAsValueTask<AnimeSerialInfoResponseDto>(new AnimeSerialInfoResponseDto())
                });
            });
        
        services.AddResiliencePipeline<string, ReWatchedAnimeSerialResponseDto>("re-watched-anime-serial-fallback",
            pipelineBuilder =>
            {
                pipelineBuilder.AddFallback(new FallbackStrategyOptions<ReWatchedAnimeSerialResponseDto>
                {
                    FallbackAction = _ =>
                        Outcome.FromResultAsValueTask<ReWatchedAnimeSerialResponseDto>(new ReWatchedAnimeSerialResponseDto())
                });
            });
        
        return services;
    }
}