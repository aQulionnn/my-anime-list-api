using System.Reflection;
using NetArchTest.Rules;

namespace AnimeService.ArchitectureTest;

public class EntityDesignTests
{
    private const string DomainEntitiesNamespace = "AnimeService.Domain.Entities";
    private readonly Assembly _assembly = typeof(AnimeService.Domain.AssemblyReference).Assembly;

    [Fact]
    public void Domain_Entities_Should_Be_Sealed()
    {
        var result = Types
            .InAssembly(_assembly)
            .That()
            .AreClasses()
            .And()
            .ResideInNamespace(DomainEntitiesNamespace)
            .Should()
            .BeSealed()
            .GetResult();
        
        Assert.True(result.IsSuccessful);
    }
}