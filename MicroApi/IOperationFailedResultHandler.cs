namespace MicroApi;

public interface IOperationFailedResultHandler<T, TParameters>
{
    Result<T> Handle(TParameters parameters);
}
