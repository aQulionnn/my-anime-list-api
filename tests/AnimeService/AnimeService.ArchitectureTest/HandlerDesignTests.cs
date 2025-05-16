using System.Reflection;
using NetArchTest.Rules;

namespace AnimeService.ArchitectureTest;

public class HandlerDesignTests
{
    private const string ApplicationFeaturesNamespace = "AnimeService.Application.Features";
    private readonly Assembly _assembly = typeof(AnimeService.Application.AssemblyReference).Assembly;
    
    [Fact]
    public void Handlers_Should_Be_Sealed()
    {
        var result = Types
            .InAssembly(_assembly)
            .That()
            .AreClasses()
            .And()
            .ResideInNamespace(ApplicationFeaturesNamespace)
            .And()
            .HaveNameEndingWith("Handler") 
            .Should()
            .BeSealed()
            .GetResult();
        
        Assert.True(result.IsSuccessful);
    }
}