using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace RiverBooks.OrderProcessing.Tests;

public class InfrastructureDependencyTests
{
    private static readonly Architecture Architecture = new ArchLoader()
        .LoadAssemblies(typeof(AssemblyInfo).Assembly)
        .Build();

    [Fact]
    public void DomainTypesShouldNotReferenceInfrastructure()
    {
        var domainTypes = Types().That()
            .ResideInNamespace("RiverBooks.OrderProcessing.Domain.*", useRegularExpressions: true);

        var infrastructureTypes = Types().That()
            .ResideInNamespace("RiverBooks.OrderProcessing.Infrastructure.*", useRegularExpressions: true);

        var rule = domainTypes.Should().NotDependOnAny(infrastructureTypes);

        rule.Check(Architecture);
    }
}
