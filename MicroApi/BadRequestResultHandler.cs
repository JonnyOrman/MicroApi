namespace MicroApi;

public class BadRequestResultHandler<T, TParameters> : ResultTypeHandler<T, InvalidParametersResult<T, TParameters>>
{
    public BadRequestResultHandler(ITypedResultTypeHandler<InvalidParametersResult<T, TParameters>> typedResultTypeHandler) : base(typedResultTypeHandler)
    {
    }
}
