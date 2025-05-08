using FranchiseService.Application.Extensions;
using FluentValidation;
using FranchiseService.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FranchiseService.Application;

public static class DependencyInjection 
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        
        services.AddAutoMapper(assembly);
        services.AddValidatorsFromAssembly(assembly);
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
        });
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggingPipelineBehavior<,>));

        services.AddResiliencePipelines();
        
        return services;
    }
}