using AnimeFranchises.Api.Middlewares;
using AnimeFranchises.Application;
using AnimeFranchises.Infrastructure;
using AnimeFranchises.Infrastructure.Extensions;
using AnimeFranchises.Presentation;
using Asp.Versioning;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Host.AddSerilogConfiguration();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPresentation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Anime Franchises API v1", Version = "v1" });
    options.SwaggerDoc("v2", new OpenApiInfo { Title = "Anime Franchises API v2", Version = "v2" });
});

builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
        options.ReportApiVersions = true;
    })
    .AddMvc()
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'V";
        options.SubstituteApiVersionInUrl = true;
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Anime Franchises API v1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "Anime Franchises API v2");
    });
}

app.MapHealthChecks("health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseOpenTelemetryPrometheusScrapingEndpoint();

app.UseMiddleware<RequestLogContextMiddleware>();

app.UseCors();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();