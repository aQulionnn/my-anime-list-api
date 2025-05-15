using System.Reflection;
using NetArchTest.Rules;

namespace ViewingService.ArchitectureTest;

public class EntityDesignTests
{
    private const string DomainEntitiesNamespace = "ViewingService.Domain.Entities";
    private readonly Assembly _assembly = typeof(ViewingService.Domain.AssemblyReference).Assembly;
    
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