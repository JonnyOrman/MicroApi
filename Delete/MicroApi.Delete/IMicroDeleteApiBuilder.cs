namespace MicroApi.Delete;

public interface IMicroDeleteApiBuilder<T, TKey> : IMicroApiBuilder<TKey, IMicroDeleteApiBuilder<T, TKey>>
{
    IMicroDeleteApiBuilder<T, TKey> MustPass<TValidationRule>()
        where TValidationRule : IValidationRule<TKey>, new();

    IMicroDeleteApiBuilder<T, TKey> OnSuccess<TSuccessEvent>()
        where TSuccessEvent : class, IOperationSuccessEvent<T, TKey>;
    IMicroDeleteApiBuilder<T, TKey> OnFailure<TFailedEvent>()
        where TFailedEvent : class, IOperationFailedEvent<TKey>;
}
