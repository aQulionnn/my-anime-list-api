using System.Reflection;
using MediatR;
using NetArchTest.Rules;

namespace FranchiseService.ArchitectureTest;

public class HandlerDesignTests
{
    private const string ApplicationFeaturesNamespace = "FranchiseService.Application.Features";
    private readonly Assembly _assembly = typeof(FranchiseService.Application.AssemblyReference).Assembly;
    
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