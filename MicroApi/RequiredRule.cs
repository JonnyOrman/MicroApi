using System.Linq.Expressions;

namespace MicroApi;

public class RequiredRule<T, TTarget> : IValidationRule<T>
{
    private readonly Expression<Func<T, TTarget>> _targetExpression;
    private readonly Func<T, TTarget> _func;

    public RequiredRule(Expression<Func<T, TTarget>> targetExpression)
    {
        _targetExpression = targetExpression;
        _func = _targetExpression.Compile();
    }

    public ValidationRuleResult Validate(T value)
    {
        var propertyValue = _func.Invoke(value);
        
        var isValid = propertyValue != null;

        if (isValid)
        {
            return new ValidationRuleResult(true);
        }

        var memberExpression = _targetExpression.Body as MemberExpression;
        
        return new InvalidPropertyRuleResult(memberExpression.Member.Name, "Value is null");
    }
}
