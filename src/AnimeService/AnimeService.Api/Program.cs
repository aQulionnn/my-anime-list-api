using AnimeService.Api.Middlewares;
using AnimeService.Application;
using AnimeService.Infrastructure;
using AnimeService.Infrastructure.Extensions;
using AnimeService.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Host.AddSerilogConfiguration();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPresentation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestLogContextMiddleware>();

app.UseCors();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();