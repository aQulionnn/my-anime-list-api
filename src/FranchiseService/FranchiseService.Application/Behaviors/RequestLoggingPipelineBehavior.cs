using FranchiseService.Domain.Shared;
using MediatR;
using Serilog;

namespace FranchiseService.Application.Behaviors;
 
 public sealed class RequestLoggingPipelineBehavior<TRequest, TResponse> 
     : IPipelineBehavior<TRequest, TResponse>
     where TRequest : class
     where TResponse : Result<TResponse>
 {
     public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
     {
         string requestName = typeof(TRequest).Name;
         
         Log.Information("Processing request {requestName} {@request}", requestName, request);
         
         TResponse response = await next();
 
         if (!response.IsSuccess)
             Log.Error("Request {requestName} failed with error {@error}", requestName, response.Error);
         else
             Log.Information("Request {requestName} succeeded with result {@data}", requestName, response.Data);
         
         return response;
     }
 }