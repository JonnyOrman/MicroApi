using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroApi.Read.Testing.UnitTests.TestClasses;

public class TestCollectionReader : IOperation<IEnumerable<TestEntity>, TestQuery>
{
    public Task<IEnumerable<TestEntity>> ExecuteAsync(TestQuery query)
    {
        throw new System.NotImplementedException();
    }
}