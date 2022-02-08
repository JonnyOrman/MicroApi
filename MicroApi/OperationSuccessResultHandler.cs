namespace MicroApi;

public class OperationSuccessResultHandler<T, TParameters> : IOperationSuccessResultHandler<T, TParameters>
{
    private readonly IOperationSuccessEventsRunner<T, TParameters> _operationSuccessEventsRunner;
    private readonly IOperationSuccessResultGenerator<T> _operationSuccessResultGenerator;

    public OperationSuccessResultHandler(
        IOperationSuccessEventsRunner<T, TParameters> operationSuccessEventsRunner,
        IOperationSuccessResultGenerator<T> operationSuccessResultGenerator
        )
    {
        _operationSuccessEventsRunner = operationSuccessEventsRunner;
        _operationSuccessResultGenerator = operationSuccessResultGenerator;
    }

    public Result<T> Handle(T entity, TParameters parameters)
    {
        _operationSuccessEventsRunner.Run(entity, parameters);

        return _operationSuccessResultGenerator.Generate(entity);
    }
}
