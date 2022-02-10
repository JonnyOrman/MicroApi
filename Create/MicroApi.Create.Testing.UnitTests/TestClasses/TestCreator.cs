using System;
using System.Threading.Tasks;

namespace MicroApi.Create.Testing.UnitTests.TestClasses;

public class TestCreator : IOperation<TestEntity, TestParameters>
{
    public Task<TestEntity> ExecuteAsync(TestParameters parameters)
    {
        throw new NotImplementedException();
    }
}
