using System.Linq.Expressions;

namespace MicroApi;

public class PropertyValidatorBuilder<TTarget, TProperty> : IPropertyValidatorBuilder<TTarget>
{
    private readonly Expression<Func<TTarget, TProperty>> _propertyExpression;
    private ICollection<IValidationRule<TTarget>> _rules;

    public PropertyValidatorBuilder(Expression<Func<TTarget, TProperty>> propertyExpression)
    {
        _propertyExpression = propertyExpression;
        _rules = new List<IValidationRule<TTarget>>();
    }

    public void IsRequired()
    {
        var requiredRule = new RequiredRule<TTarget, TProperty>(_propertyExpression);

        _rules.Add(requiredRule);
    }

    public IEnumerable<IValidationRule<TTarget>> ToRules()
    {
        return _rules;
    }

    public void MustPass<TValidationRule>()
        where TValidationRule : IValidationRule<TTarget>, new()
    {
        var rule = new TValidationRule();

        _rules.Add(rule);
    }
}
