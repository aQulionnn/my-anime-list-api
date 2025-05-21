using NetArchTest.Rules;

namespace FranchiseService.ArchitectureTest;

public class LayerDependencyTests
{
    private const string DomainNamespace = "FranchiseService.Domain";
    private const string ApplicationNamespace = "FranchiseService.Application";
    private const string InfrastructureNamespace = "FranchiseService.Infrastructure";
    private const string PresentationNamespace = "FranchiseService.Presentation";
    private const string ApiNamespace = "FranchiseService.Api";

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
        var assembly = typeof(FranchiseService.Domain.AssemblyReference).Assembly;

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
        var assembly = typeof(FranchiseService.Application.AssemblyReference).Assembly;

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
        var assembly = typeof(FranchiseService.Infrastructure.AssemblyReference).Assembly;

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
        var assembly = typeof(FranchiseService.Presentation.AssemblyReference).Assembly;

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