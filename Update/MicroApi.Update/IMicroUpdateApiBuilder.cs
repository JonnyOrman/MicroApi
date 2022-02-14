namespace MicroApi.Update;

public interface IMicroUpdateApiBuilder<T, TParameters> : IMicroApiBuilder<TParameters, IMicroUpdateApiBuilder<T, TParameters>>
{
    IMicroUpdateApiBuilder<T, TParameters> MustPass<TValidationRule>()
        where TValidationRule : IValidationRule<TParameters>, new();

    IMicroUpdateApiBuilder<T, TParameters> OnSuccess<TSuccessEvent>()
        where TSuccessEvent : class, IOperationSuccessEvent<T, TParameters>;
    IMicroUpdateApiBuilder<T, TParameters> OnFailure<TFailedEvent>()
        where TFailedEvent : class, IOperationFailedEvent<TParameters>;
}
