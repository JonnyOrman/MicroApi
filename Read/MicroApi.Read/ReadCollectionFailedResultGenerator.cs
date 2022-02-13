namespace MicroApi.Read;

public class ReadCollectionFailedResultGenerator<T, TQuery> : IOperationFailedResultGenerator<T, TQuery>
{
    public Result<T> Generate(TQuery parameters)
    {
        throw new NotImplementedException();
    }
}
