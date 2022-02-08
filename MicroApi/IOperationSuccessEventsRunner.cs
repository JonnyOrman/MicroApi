namespace MicroApi;

public interface IOperationSuccessEventsRunner<T, TParameters>
{
    void Run(T entity, TParameters parameters);
}
