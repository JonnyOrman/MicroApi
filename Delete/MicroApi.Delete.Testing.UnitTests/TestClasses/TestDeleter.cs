using System;
using System.Threading.Tasks;

namespace MicroApi.Delete.Testing.UnitTests.TestClasses;

public class TestDeleter : IOperation<TestEntity, string>
{
    public Task<TestEntity> ExecuteAsync(string key)
    {
        throw new NotImplementedException();
    }
}
