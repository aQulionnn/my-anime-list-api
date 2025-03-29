using Serilog;
using Serilog.Context;

namespace AnimeFranchises.Api.Middlewares;

public class RequestLogContextMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        using (LogContext.PushProperty("CorrelationId", context.TraceIdentifier))
        {
            Log.Information("Incoming request: {Method} {Path}", context.Request.Method, context.Request.Path);

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An exception occurred while processing the request.");
                throw;
            }
            
            Log.Information("Response status: {StatusCode}", context.Response.StatusCode);
        }
    }
}