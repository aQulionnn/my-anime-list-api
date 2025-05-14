using NetArchTest.Rules;

namespace AnimeService.ArchitectureTest;

public class LayerDependencyTests
{
    private const string DomainNamespace = "AnimeService.Domain";
    private const string ApplicationNamespace = "AnimeService.Application";
    private const string InfrastructureNamespace = "AnimeService.Infrastructure";
    private const string PresentationNamespace = "AnimeService.Presentation";
    private const string ApiNamespace = "AnimeService.Api";
    
    [Fact]
    public void Domain_Should_Not_HaveDependencyOnLayers()
    {
        var assembly = typeof(AnimeService.Domain.AssemblyReference).Assembly;

        var layers = new[]
        {
            ApplicationNamespace,
            InfrastructureNamespace,
            PresentationNamespace,
            ApiNamespace
        };

        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(layers)
            .GetResult();
        
        Assert.True(result.IsSuccessful);
    }
    
    [Fact]
    public void Application_Should_Not_HaveDependencyOnLayers()
    {
        var assembly = typeof(AnimeService.Application.AssemblyReference).Assembly;

        var layers = new[]
        {
            InfrastructureNamespace,
            PresentationNamespace,
            ApiNamespace
        };

        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(layers)
            .GetResult();
        
        Assert.True(result.IsSuccessful);
    }
    
    [Fact]
    public void Infrastructure_Should_Not_HaveDependencyOnLayers()
    {
        var assembly = typeof(AnimeService.Infrastructure.AssemblyReference).Assembly;

        var layers = new[]
        {
            PresentationNamespace,
            ApiNamespace
        };

        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(layers)
            .GetResult();
        
        Assert.True(result.IsSuccessful);
    }
    
    [Fact]
    public void Presentation_Should_Not_HaveDependencyOnLayers()
    {
        var assembly = typeof(AnimeService.Presentation.AssemblyReference).Assembly;

        var layers = new[]
        {
            InfrastructureNamespace,
            ApiNamespace
        };

        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(layers)
            .GetResult();
        
        Assert.True(result.IsSuccessful);
    }
}