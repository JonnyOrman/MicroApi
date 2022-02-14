using System;
using System.Threading.Tasks;

namespace MicroApi.Update.Testing.UnitTests.TestClasses;

public class TestUpdater : IOperation<TestEntity, TestParameters>
{
    public Task<TestEntity> ExecuteAsync(TestParameters parameters)
    {
        throw new NotImplementedException();
    }
}
