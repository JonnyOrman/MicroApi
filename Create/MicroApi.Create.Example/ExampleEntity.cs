using MicroApi.Core;

namespace MicroApi.Create.Example;

public class ExampleEntity : Entity<int>
{
    public ExampleEntity(
        int key,
        string name,
        int value
        ) : base(key)
    {
        Name = name;
        Value = value;
    }

    public string Name { get; }
    
    public int Value { get; }
}
