namespace MicroApi.Core;

public class SuccessResultHandler<T> : ResultTypeHandler<T, SuccessResult<T>>
{
    public SuccessResultHandler(ITypedResultTypeHandler<SuccessResult<T>> typedResultTypeHandler) : base(typedResultTypeHandler)
    {
    }
}
