namespace MicroApi.Core;

public class ValidParametersHandler<T, TParameters, TOperator> : IValidParametersHandler<T, TParameters>
{
    private readonly TOperator _entityOperator;
    private readonly Func<TOperator, TParameters, Task<T>> _operatorFunc;
    private readonly IOperationResultHandler<T, TParameters> _operationResultHandler;

    public ValidParametersHandler(
        TOperator entityOperator,
        Func<TOperator, TParameters, Task<T>> operatorFunc,
        IOperationResultHandler<T, TParameters> operationResultHandler)
    {
        _entityOperator = entityOperator;
        _operatorFunc = operatorFunc;
        _operationResultHandler = operationResultHandler;
    }

    public async Task<Result<T>> HandleAsync(TParameters parameters)
    {
        var result = await _operatorFunc.Invoke(_entityOperator, parameters);

        return _operationResultHandler.Handle(result, parameters);
    }
}
