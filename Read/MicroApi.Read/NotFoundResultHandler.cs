namespace MicroApi.Read;

public class NotFoundResultHandler<T, TKey> : ResultTypeHandler<T, NotFoundResult<T, TKey>>
{
    public NotFoundResultHandler(ITypedResultTypeHandler<NotFoundResult<T, TKey>> typedResultTypeHandler) : base(typedResultTypeHandler)
    {
    }
}
