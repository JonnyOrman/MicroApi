namespace MicroApi;

public class OperationFailedResultHandler<T, TParameters> : IOperationFailedResultHandler<T, TParameters>
{
    private readonly IOperationFailedEventsRunner<TParameters> _operationFailedEventsRunner;
    private readonly IOperationFailedResultGenerator<T, TParameters> _operationFailedResultGenerator;

    public OperationFailedResultHandler(
        IOperationFailedEventsRunner<TParameters> operationFailedEventsRunner,
        IOperationFailedResultGenerator<T, TParameters> operationFailedResultGenerator
    )
    {
        _operationFailedEventsRunner = operationFailedEventsRunner;
        _operationFailedResultGenerator = operationFailedResultGenerator;
    }

    public Result<T> Handle(TParameters parameters)
    {
        _operationFailedEventsRunner.Run(parameters);

        return _operationFailedResultGenerator.Generate(parameters);
    }
}
