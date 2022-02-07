using System.Linq.Expressions;

namespace MicroApi;

public interface IMicroApiBuilder<TParameters>
{
    IMicroApiBuilder<TParameters> Where<TProperty>(Expression<Func<TParameters, TProperty>> propertyExpression);

    IMicroApiBuilder<TParameters> IsRequired();

    void Start();

    IMicroApiBuilder<TParameters> MustPass<TValidationRule>()
        where TValidationRule : IValidationRule<TParameters>, new();
}
