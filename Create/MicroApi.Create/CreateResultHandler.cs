namespace MicroApi.Create;

public class CreateResultHandler<T, TParameters> : IOperationResultHandler<T, TParameters>
{
    public Result<T> Handle(T result, TParameters parameters)
    {
        return new SuccessResult<T>(result);
    }
}
