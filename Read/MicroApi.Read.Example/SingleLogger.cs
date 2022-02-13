namespace MicroApi.Read.Example;

public class SingleLogger : IOperationSuccessEvent<ExampleEntity, int>
{
    public void Run(ExampleEntity entity, int key)
    {
        Console.WriteLine($"Got {nameof(ExampleEntity)} with {nameof(ExampleEntity.Key)} {key}");
    }
}
