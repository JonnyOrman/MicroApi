namespace MicroApi;

public interface IOperationSuccessResultHandler<T, TParameters>
{
    Result<T> Handle(T entity, TParameters parameters);
}
