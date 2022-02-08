namespace MicroApi;

public class OperationSuccessEventsRunner<T, TParameters> : IOperationSuccessEventsRunner<T, TParameters>
{
    private readonly IEnumerable<IOperationSuccessEvent<T, TParameters>> _operationSuccessEvents;

    public OperationSuccessEventsRunner(
        IEnumerable<IOperationSuccessEvent<T, TParameters>> operationSuccessEvents
        )
    {
        _operationSuccessEvents = operationSuccessEvents;
    }

    public void Run(T entity, TParameters parameters)
    {
        foreach (var operationSuccessEvent in _operationSuccessEvents)
        {
            operationSuccessEvent.Run(entity, parameters);
        }
    }
}
