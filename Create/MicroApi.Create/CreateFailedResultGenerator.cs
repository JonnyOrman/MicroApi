namespace MicroApi.Create;

public class CreateFailedResultGenerator<T, TParameters> : IOperationFailedResultGenerator<T, TParameters>
{
    public Result<T> Generate(TParameters parameters)
    {
        throw new NotImplementedException();
    }
}
