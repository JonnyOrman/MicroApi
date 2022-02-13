namespace MicroApi.Create;

public interface IMicroCreateApiBuilder<T, TParameters> : IMicroApiBuilder<TParameters, IMicroCreateApiBuilder<T, TParameters>>
{
    IMicroCreateApiBuilder<T, TParameters> MustPass<TValidationRule>()
        where TValidationRule : IValidationRule<TParameters>, new();

    IMicroCreateApiBuilder<T, TParameters> OnSuccess<TSuccessEvent>()
        where TSuccessEvent : class, IOperationSuccessEvent<T, TParameters>;
    IMicroCreateApiBuilder<T, TParameters> OnFailure<TFailedEvent>()
        where TFailedEvent : class, IOperationFailedEvent<TParameters>;
}
