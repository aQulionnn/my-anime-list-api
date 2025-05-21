using NetArchTest.Rules;

namespace ViewingService.ArchitectureTest;

public class LayerDependencyTests
{
    private const string DomainNamespace = "ViewingService.Domain";
    private const string ApplicationNamespace = "ViewingService.Application";
    private const string InfrastructureNamespace = "ViewingService.Infrastructure";
    private const string PresentationNamespace = "ViewingService.Presentation";
    private const string ApiNamespace = "ViewingService.Api";
    
    [Fact]
    public void MessageBroker_Should_Not_HaveDependencyOnLayers()
    {
        var assembly = typeof(MessageBroker.AssemblyReference).Assembly;

        var layers = new[]
        {
            DomainNamespace,
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
    public void SharedKernel_Should_Not_HaveDependencyOnLayers()
    {
        var assembly = typeof(SharedKernel.AssemblyReference).Assembly;

        var layers = new[]
        {
            DomainNamespace,
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
    public void Domain_Should_Not_HaveDependencyOnLayers()
    {
        var assembly = typeof(ViewingService.Domain.AssemblyReference).Assembly;

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
        var assembly = typeof(ViewingService.Application.AssemblyReference).Assembly;

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
        var assembly = typeof(ViewingService.Infrastructure.AssemblyReference).Assembly;

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
        var assembly = typeof(ViewingService.Presentation.AssemblyReference).Assembly;

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