namespace MicroApi;

public class Validator<T> : IValidator<T>
{
    private readonly IValidationRuleResultsCalculator<T> _validationRuleResultsCalculator;
    private readonly IValidationResultGenerator<T> _validationResultGenerator;

    public Validator(
        IValidationRuleResultsCalculator<T> validationRuleResultsCalculator,
        IValidationResultGenerator<T> validationResultGenerator
        )
    {
        _validationRuleResultsCalculator = validationRuleResultsCalculator;
        _validationResultGenerator = validationResultGenerator;
    }

    public ValidationResult Validate(T value)
    {
        var ruleResults = _validationRuleResultsCalculator.Calculate(value);

        return _validationResultGenerator.Generate(ruleResults);
    }
}
