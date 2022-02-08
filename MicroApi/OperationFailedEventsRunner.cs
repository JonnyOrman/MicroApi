namespace MicroApi;

public class OperationFailedEventsRunner<TParameters> : IOperationFailedEventsRunner<TParameters>
{
    private readonly IEnumerable<IOperationFailedEvent<TParameters>> _operationFailedEvents;

    public OperationFailedEventsRunner(
        IEnumerable<IOperationFailedEvent<TParameters>> operationFailedEvents
    )
    {
        _operationFailedEvents = operationFailedEvents;
    }

    public void Run(TParameters parameters)
    {
        foreach (var operationFailedEvent in _operationFailedEvents)
        {
            operationFailedEvent.Run(parameters);
        }
    }
}
