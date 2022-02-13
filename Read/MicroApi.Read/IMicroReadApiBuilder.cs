namespace MicroApi.Read;

public interface IMicroReadApiBuilder<T, TKey, TQuery> : IMicroApiBuilder<TQuery, IMicroReadApiBuilder<T, TKey, TQuery>>
{
    IMicroReadApiBuilder<T, TKey, TQuery> MustPass<TValidationRule>()
        where TValidationRule : IValidationRule<TQuery>, new();

    IMicroReadApiBuilder<T, TKey, TQuery> OnGetSingleSuccess<TSuccessEvent>()
        where TSuccessEvent : class, IOperationSuccessEvent<T, TKey>;
    IMicroReadApiBuilder<T, TKey, TQuery> OnGetSingleFailure<TFailedEvent>()
        where TFailedEvent : class, IOperationFailedEvent<TKey>;

    IMicroReadApiBuilder<T, TKey, TQuery> OnGetCollectionSuccess<TSuccessEvent>()
        where TSuccessEvent : class, IOperationSuccessEvent<IEnumerable<T>, TQuery>;
    IMicroReadApiBuilder<T, TKey, TQuery> OnGetCollectionFailure<TFailedEvent>()
        where TFailedEvent : class, IOperationFailedEvent<TQuery>;

}
