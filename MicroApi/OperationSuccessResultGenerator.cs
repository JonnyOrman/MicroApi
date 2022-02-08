namespace MicroApi;

public class OperationSuccessResultGenerator<T> : IOperationSuccessResultGenerator<T>
{
    public Result<T> Generate(T entity)
    {
        return new SuccessResult<T>(entity);
    }
}
