namespace MicroApi;

public interface IOperationFailedResultGenerator<T, TParameters>
{
    Result<T> Generate(TParameters parameters);
}
