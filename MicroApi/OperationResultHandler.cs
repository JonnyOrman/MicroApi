namespace MicroApi;

public class OperationResultHandler<T, TParameters> : IOperationResultHandler<T, TParameters>
{
    private readonly IOperationSuccessResultHandler<T, TParameters> _operationSuccessResultHandler;
    private readonly IOperationFailedResultHandler<T, TParameters> _operationFailedResultHandler;

    public OperationResultHandler(
        IOperationSuccessResultHandler<T, TParameters> operationSuccessResultHandler,
        IOperationFailedResultHandler<T, TParameters> operationFailedResultHandler
        )
    {
        _operationSuccessResultHandler = operationSuccessResultHandler;
        _operationFailedResultHandler = operationFailedResultHandler;
    }

    public Result<T> Handle(T result, TParameters parameters)
    {
        if (result != null)
        {
            return _operationSuccessResultHandler.Handle(result, parameters);
        }

        return _operationFailedResultHandler.Handle(parameters);
    }
}
