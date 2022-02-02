using System;
using System.Threading.Tasks;

namespace MicroApi.Create.Testing.UnitTests.TestClasses;

public class TestCreator : ICreator<TestEntity, TestParameters>
{
    public Task<TestEntity> Create(TestParameters parameters)
    {
        throw new NotImplementedException();
    }
}
