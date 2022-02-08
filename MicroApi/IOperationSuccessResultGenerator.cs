namespace MicroApi;

public interface IOperationSuccessResultGenerator<T>
{
    Result<T> Generate(T entity);
}
