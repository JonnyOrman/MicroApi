using System.Threading.Tasks;

namespace MicroApi.Core.Testing.UnitTests.TestClasses;

public class TestOperator
{
    private readonly TestEntity _testEntity;

    public TestOperator(TestEntity testEntity)
    {
        _testEntity = testEntity;
    }

    public async Task<TestEntity> Execute(TestParameters testParameters)
    {
        return _testEntity;
    }
}
