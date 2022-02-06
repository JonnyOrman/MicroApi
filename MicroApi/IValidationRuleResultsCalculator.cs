namespace MicroApi;

public interface IValidationRuleResultsCalculator<T>
{
    IEnumerable<ValidationRuleResult> Calculate(T value);
}
