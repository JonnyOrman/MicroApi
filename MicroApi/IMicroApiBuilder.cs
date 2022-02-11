using System.Linq.Expressions;

namespace MicroApi;

public interface IMicroApiBuilder<TParameters, TBuilder>
{
    void Start();

    TBuilder Where<TProperty>(Expression<Func<TParameters, TProperty>> propertyExpression);

    TBuilder IsRequired();
}
