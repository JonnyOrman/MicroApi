namespace MicroApi.Core;

public class ValidParametersHandler<T, TParameters, TOperator> : IValidParametersHandler<T, TParameters>
{
    private readonly TOperator _entityOperator;
    private readonly Func<TOperator, TParameters, Task<T>> _operatorFunc;

    public ValidParametersHandler(
        TOperator entityOperator,
        Func<TOperator, TParameters, Task<T>> operatorFunc)
    {
        _entityOperator = entityOperator;
        _operatorFunc = operatorFunc;
    }

    public async Task<Result<T>> HandleAsync(TParameters parameters)
    {
        var result = await _operatorFunc.Invoke(_entityOperator, parameters);

        return new SuccessResult<T>(result);
    }
}
