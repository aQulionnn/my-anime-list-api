using MediatR;
using Serilog;

namespace AnimeSeries.Application.Behaviors;

public sealed class RequestLoggingPipelineBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : class
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;
        
        Log.Information("Processing request {requestName} {@request}", requestName, request);
        
        TResponse response = await next();

        return response;
    }
}