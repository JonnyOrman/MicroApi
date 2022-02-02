namespace MicroApi.Core;

public class ValidationResultGenerator<T> : IValidationResultGenerator<T>
{
    public ValidationResult Generate(IEnumerable<ValidationRuleResult> validationRuleResults)
    {
        var isSuccessful = true;

        var invalidProperties = new Dictionary<string, List<string>>();

        foreach (var validationRuleResult in validationRuleResults)
        {
            if (!validationRuleResult.IsSuccessful)
            {
                isSuccessful = false;

                var invalidRuleResult = validationRuleResult as InvalidPropertyRuleResult;

                if (!invalidProperties.ContainsKey(invalidRuleResult.PropertyName))
                {
                    invalidProperties[invalidRuleResult.PropertyName] = new List<string>();
                }

                invalidProperties[invalidRuleResult.PropertyName].Add(invalidRuleResult.Message);
            }
        }

        if (isSuccessful)
        {
            return new ValidResult();
        }

        var invalidPropertyValues = invalidProperties.Select(x => new InvalidPropertyValue(x.Key, x.Value));

        return new InvalidResult(
            invalidPropertyValues
            );
    }
}
