namespace MicroApi.Read.Example;

public class TypeRule : IValidationRule<ExampleQuery>
{
    public ValidationRuleResult Validate(ExampleQuery value)
    {
        var isValid = !string.IsNullOrWhiteSpace(value.Type);

        if (isValid)
        {
            return new ValidationRuleResult(true);
        }

        return new InvalidPropertyRuleResult(nameof(value.Type), $"{nameof(value.Type)} must be provided");
    }
}
