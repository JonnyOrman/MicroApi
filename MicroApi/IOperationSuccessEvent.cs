namespace MicroApi;

public interface IOperationSuccessEvent<T, TParameters>
{
    void Run(T entity, TParameters parameters);
}
