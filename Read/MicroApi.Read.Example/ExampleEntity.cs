using MicroApi.Core;

namespace MicroApi.Read.Example;

public class ExampleEntity : Entity<int>
{
    public ExampleEntity(int key) : base(key)
    {
    }
}