namespace MicroApi;

public class ValidationResultGenerator<T> : IValidationResultGenerator<T>
{
    private readonly IValidationResultBuilderCreator _validationResultBuilderCreator;

    public ValidationResultGenerator(
        IValidationResultBuilderCreator validationResultBuilderCreator
        )
    {
        _validationResultBuilderCreator = validationResultBuilderCreator;
    }

    public ValidationResult Generate(IEnumerable<ValidationRuleResult> validationRuleResults)
    {
        var validationResultBuilder = _validationResultBuilderCreator.Create();

        foreach (var validationRuleResult in validationRuleResults)
        {
            validationResultBuilder.Add(validationRuleResult);
        }

        return validationResultBuilder.Build();
    }
}
