namespace MicroApi;

public class ValidationRuleResultsCalculator<T> : IValidationRuleResultsCalculator<T>
{
    private readonly IEnumerable<IValidationRule<T>> _validationRules;

    public ValidationRuleResultsCalculator(
        IEnumerable<IValidationRule<T>> validationRules
        )
    {
        _validationRules = validationRules;
    }

    public IEnumerable<ValidationRuleResult> Calculate(T entity)
    {
        return _validationRules.Select(x => x.Validate(entity));
    }
}
