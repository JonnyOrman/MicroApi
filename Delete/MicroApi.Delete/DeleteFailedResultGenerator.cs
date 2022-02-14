namespace MicroApi.Delete;

public class DeleteFailedResultGenerator<T, TKey> : IOperationFailedResultGenerator<T, TKey>
{
    public Result<T> Generate(TKey parameters)
    {
        throw new NotImplementedException();
    }
}
