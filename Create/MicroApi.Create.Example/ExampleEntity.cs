namespace MicroApi.Create.Example;

public class ExampleEntity : Entity<int>
{
    public ExampleEntity(
        int key,
        string name,
        string type
        ) : base(key)
    {
        Name = name;
        Type = type;
    }

    public string Name { get; }
    
    public string Type { get; }
}
