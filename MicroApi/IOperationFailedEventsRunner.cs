namespace MicroApi;

public interface IOperationFailedEventsRunner<TParameters>
{
    void Run(TParameters parameters);
}
