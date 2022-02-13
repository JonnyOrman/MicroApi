namespace MicroApi.Read.Example;

public class CollectionLogger : IOperationSuccessEvent<IEnumerable<ExampleEntity>, ExampleQuery>
{
    public void Run(IEnumerable<ExampleEntity> entity, ExampleQuery parameters)
    {
        Console.WriteLine($"Got all {nameof(ExampleEntity)} entities where {nameof(ExampleQuery.Type)} is {parameters.Type}");
    }
}
