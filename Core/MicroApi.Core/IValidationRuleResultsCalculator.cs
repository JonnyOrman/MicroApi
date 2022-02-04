namespace MicroApi.Core;

public interface IValidationRuleResultsCalculator<T>
{
    IEnumerable<ValidationRuleResult> Calculate(T value);
}
