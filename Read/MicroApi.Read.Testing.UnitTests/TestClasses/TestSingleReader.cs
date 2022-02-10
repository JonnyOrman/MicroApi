using System.Threading.Tasks;

namespace MicroApi.Read.Testing.UnitTests.TestClasses;

public class TestSingleReader : IOperation<TestEntity, string>
{
    public Task<TestEntity> ExecuteAsync(string parameters)
    {
        throw new System.NotImplementedException();
    }
}