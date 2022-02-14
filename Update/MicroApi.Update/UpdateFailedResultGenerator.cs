namespace MicroApi.Update;

public class UpdateFailedResultGenerator<T, TParameters> : IOperationFailedResultGenerator<T, TParameters>
{
    public Result<T> Generate(TParameters parameters)
    {
        throw new NotImplementedException();
    }
}
