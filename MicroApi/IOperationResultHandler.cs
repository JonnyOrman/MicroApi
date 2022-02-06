namespace MicroApi;

public interface IOperationResultHandler<T, TParameters>
{
    Result<T> Handle(T result, TParameters parameters);
}
