using System.Linq.Expressions;

namespace MicroApi;

public interface IMicroApiBuilder<T, TParameters>
{
    IMicroApiBuilder<T, TParameters> Where<TProperty>(Expression<Func<TParameters, TProperty>> propertyExpression);

    IMicroApiBuilder<T, TParameters> IsRequired();

    void Start();

    IMicroApiBuilder<T, TParameters> MustPass<TValidationRule>()
        where TValidationRule : IValidationRule<TParameters>, new();

    IMicroApiBuilder<T, TParameters> OnSuccess<TSuccessEvent>()
        where TSuccessEvent : class, IOperationSuccessEvent<T, TParameters>;
    IMicroApiBuilder<T, TParameters> OnFailure<TFailedEvent>()
        where TFailedEvent : class, IOperationFailedEvent<TParameters>;
}
