namespace MicroApi;

public interface IOperationFailedEvent<TParameters>
{
    void Run(TParameters parameters);
}
