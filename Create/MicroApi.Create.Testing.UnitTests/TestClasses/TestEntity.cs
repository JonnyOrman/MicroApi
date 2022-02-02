using MicroApi.Core;

namespace MicroApi.Create.Testing.UnitTests.TestClasses;

public class TestEntity : Entity<string>
{
    public TestEntity(string key) : base(key)
    {
    }
}
